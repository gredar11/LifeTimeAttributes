# LifetimeAttributes

This library was created to make service registration into DI container easier. 
Advantages:
- You no longer need to write a long registration code in a separate file ``ServiceCollectionExtensions``.
- It is easy to understand what kind of life cycle the service has

## Plans:

### Functionality

- [x] Attributes of different life cycles

- [x] Registration of tagged services

### Static analysis:

- [x] Checking the implementation by the class of the interface specified in the attribute

- [ ] Checking that if the registered class has a Singleton life cycle, so that the infected classes also have such a cycle