# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog][], and this project adheres to [Semantic Versioning][].  

[Keep a Changelog](https://keepachangelog.com/en/1.0.0/)
[Semantic Versioning](https://semver.org/spec/v2.0.0.html)

## [Unreleased]

### Added
 -  A game-event scriptable-object (with or without data) that can be easly created via Unity Editor.
 -  Abstract event-listener monobehaviour classes, that can be extended to easly define components that 
    listen to ONE event (with or without data).
 -  A game-event trigger component that fire an event when its parent object enters, stays or exits a 
    collision with another object.
 -  A custom inspector for the game-event scriptable-objects that allow to manually raise the event via 
    Unity Editor.
 -  Unit tests for the Event classes (with or without data).
