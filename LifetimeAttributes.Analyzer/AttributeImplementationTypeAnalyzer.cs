using System.Collections.Immutable;
using System.Linq;
using LifetimeAttributes.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace LifetimeAttributes.Analyzer;

/// <summary>
/// Analyzer reports if class doesn't implement interface defined in BaseLifetimeAttribute
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class AttributeImplementationTypeAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "LT0001";

    private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.LT0001Title),
        Resources.ResourceManager, typeof(Resources));

    private static readonly LocalizableString MessageFormat =
        new LocalizableResourceString(nameof(Resources.LT0001MessageFormat), Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString Description =
        new LocalizableResourceString(nameof(Resources.LT0001Description), Resources.ResourceManager,
            typeof(Resources));

    private const string Category = "ServiceLifetime";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category,
        DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);
    }

    /// <summary>
    /// Executed for each Syntax Node with 'SyntaxKind' is 'ClassDeclaration'.
    /// </summary>
    /// <param name="syntaxNodeAnalysisContext">Operation context.</param>
    private void AnalyzeSyntax(SyntaxNodeAnalysisContext syntaxNodeAnalysisContext)
    {
        if (syntaxNodeAnalysisContext.ContainingSymbol is not ITypeSymbol typeSymbol)
        {
            return;
        }

        var attributes = typeSymbol.GetAttributes();

        var interfaces = typeSymbol.Interfaces.Select(@interface => @interface.Name).ToImmutableHashSet();

        foreach (var attributeData in attributes)
        {
            if (!TryGetNonImplementedInterfaces(interfaces, attributeData, out var nonImplementedInterface))
            {
                continue;
            }

            // if class doesn't implement attribute declared interface we need to report
            var location = attributeData.ApplicationSyntaxReference?.GetSyntax().GetLocation();
            if (location is null)
            {
                continue;
            }

            var diagnostic = Diagnostic.Create(Rule, location, nonImplementedInterface);
            syntaxNodeAnalysisContext.ReportDiagnostic(diagnostic);
        }
    }

    private bool TryGetNonImplementedInterfaces(ImmutableHashSet<string> interfacesNames, AttributeData attributeData, out string interfaceName)
    {
        interfaceName = string.Empty;

        if (attributeData.AttributeClass?.BaseType?.Name != nameof(BaseLifetimeAttribute))
        {
            return false;
        }

        var firstArgument = attributeData.ConstructorArguments.First();

        if (firstArgument.Value is not INamedTypeSymbol namedTypeSymbol)
        {
            return false;
        }

        if (interfacesNames.Contains(namedTypeSymbol.Name))
        {
            return false;
        }

        interfaceName = namedTypeSymbol.Name;
        return true;
    }
}
