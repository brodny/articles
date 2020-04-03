# Loose coupling to test easier

Tight coupling is a situation where a component has a lot of knowledge of other separate components. It usually implies that it depends on too much details of other components.

A situation like that could really complicate creating unit tests. It could be really hard to create an instance of a tightly-coupled component in a test code. Imagine that in order to test your class, you have to create a few different classes that communicate with the database, read files and perform web requests. Oh, of course, mocking concrete classes is not an option. It is of course possible, but requires a lot of setup and boilerplate code and could be unreliable (e.g. because said web server is currently unavailable).

Such a situation is probably not very often seen while creating a new code, especially using TDD approach. Much more often you can find it in a legacy code. You are asked to fix some bug or provide extra functionality to some part of a system and you would like to test that. You see that the piece of system has not yet been tested, so you think about providing unit tests for the unchanged parts of the component as well... and then you hit the wall. The wall built of all these other classes and dependencies.

