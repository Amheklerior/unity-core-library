# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog][], and this project adheres to [Semantic Versioning][].  

[Keep a Changelog]: https://keepachangelog.com/en/1.0.0/
[Semantic Versioning]: https://semver.org/spec/v2.0.0.html


## [Unreleased]
### Added
 - 	The `Timer` class 
 - 	Unit tests for the `Timer` class
 

## [0.0.2] - 2020-05-26
### Added
 - 	A generic `Pool` class, designed for highly performat operations.
 - 	The `GameObjectPool` monobehaviour component, that allow to quickly create and setup an object pool 
 	via Unity Editor.
 -  Unit tests for the `Pool` class.
 -	The `RichMonoBehaviour` and `RichScriptableObject` classes, that define a unified interface for all 
	components and scriptable-objects with a description and a debugMode flag to toggle console logs.

### Changed
 -	The `GameEventListener` does not create an event instance when no event is provided by the user. 
	If no event is provided, a `MissingReferenceException` is thrown.
 -	The `GameEvent` and `GameEvent<T>` scriptable-objects now extends the `RichScriptableObject`.
 -	Improved logs readability in `DebuggableEvent` and `DebuggableEvent<T>`.


## [0.0.1] - 2020-05-24
### Added
 - 	A generic `Event` class, that defines events that can carry data.
 -  A `GameEvent` scriptable-object (with or without data) that can be easly created via Unity Editor.
 -  Abstract `EventListener` monobehaviour classes, that can be extended to easly define components that 
    listen to ONE event (with or without data).
 -  A `GameEventTrigger` component that fire an event when its parent object enters, stays or exits a 
    collision with another object.
 -  A custom inspector for the `GameEvent` scriptable-object that allow to manually raise the event via 
    Unity Editor for easy testing.
 -  Unit tests for the `Event` classes (with or without data).


[Unreleased]: https://github.com/Amheklerior/unity-core-library/compare/0.0.2...HEAD
[0.0.2]: https://github.com/Amheklerior/unity-core-library/compare/0.0.1...0.0.2
[0.0.1]: https://github.com/Amheklerior/unity-core-library/tree/0.0.1