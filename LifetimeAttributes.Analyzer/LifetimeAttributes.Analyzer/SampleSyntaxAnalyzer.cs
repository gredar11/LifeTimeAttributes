using System.Collections.Immutable;
using System.Linq;
using LifetimeAttributes.Attributes;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace LifetimeAttributes.Analyzer;

/// <summary>
/// A sample analyzer that reports the company name being used in class declarations.
/// Traverses through the Syntax Tree and checks the name (identifier) of each class node.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class SampleSyntaxAnalyzer : DiagnosticAnalyzer
{
    // Preferred format of DiagnosticId is Your Prefix + Number, e.g. CA1234.
    public const string DiagnosticId = "LT0001";

    // Feel free to use raw strings if you don't need localization.
    private static readonly LocalizableString Title = new LocalizableResourceString(nameof(Resources.LT0001Title),
        Resources.ResourceManager, typeof(Resources));

    // The message that will be displayed to the user.
    private static readonly LocalizableString MessageFormat =
        new LocalizableResourceString(nameof(Resources.LT0001MessageFormat), Resources.ResourceManager,
            typeof(Resources));

    private static readonly LocalizableString Description =
        new LocalizableResourceString(nameof(Resources.LT0001Description), Resources.ResourceManager,
            typeof(Resources));

    // The category of the diagnostic (Design, Naming etc.).
    private const string Category = "Naming";

    private static readonly DiagnosticDescriptor Rule = new(DiagnosticId, Title, MessageFormat, Category,
        DiagnosticSeverity.Warning, isEnabledByDefault: true, description: Description);

    // Keep in mind: you have to list your rules here.
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } =
        ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        // You must call this method to avoid analyzing generated code.
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);

        // You must call this method to enable the Concurrent Execution.
        context.EnableConcurrentExecution();

        // Subscribe to the Syntax Node with the appropriate 'SyntaxKind' (ClassDeclaration) action.
        // To figure out which Syntax Nodes you should choose, consider installing the Roslyn syntax tree viewer plugin Rossynt: https://plugins.jetbrains.com/plugin/16902-rossynt/
        context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.ClassDeclaration);

        // Check other 'context.Register...' methods that might be helpful for your purposes.
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

        var interfaces = typeSymbol.Interfaces;

        foreach (var attributeData in attributes)
        {
            if (attributeData.AttributeClass?.BaseType?.Name != nameof(BaseLifetimeAttribute))
            {
                continue;
            }

            var firstArgument = attributeData.ConstructorArguments.First();

            if (firstArgument.Value is not INamedTypeSymbol namedTypeSymbol)
            {
                continue;
            }

            if (interfaces.Any(classInterface => classInterface.BaseType?.Name == namedTypeSymbol.Name))
            {
                continue;
            }

            // if class doesn't implement attribute declared interface we need to report
            var location = attributeData.ApplicationSyntaxReference?.GetSyntax().GetLocation();
            if (location is null)
            {
                continue;
            }

            var diagnostic = Diagnostic.Create(Rule,
                // The highlighted area in the analyzed source code. Keep it as specific as possible.
                location,
                namedTypeSymbol.Name
            );
            syntaxNodeAnalysisContext.ReportDiagnostic(diagnostic);
        }
    }
}
