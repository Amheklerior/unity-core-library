# Unity Core Library
The Unity Core Library is a unity package that defines core components and scripts that can be reused across multiple unity projects.  
This library is designed with two simple principles in mind:  

 - __Easy to use__: The vast majority of components are thought to be easily created and used via the Unity Editor, 
   therefore no programming background is required to use it (provided familiarity with the engine), encouraging 
   _team collaboration_ and _fast prototyping_. 
 - __Extensible__: Each project has its own specific needs, therefore the concept of _a single library to solve them all_ is, 
   well, a good fantasy incipit. Feel free to extend the classes and the components defined to meet your specific needs.   



## Table of Contents
 - [Features][]
 - [Getting Started][]
 - [Requirements][]
 - [Roadmap][] (WIP)
 - [Support][]
 - [Licence][]

[Features]: https://github.com/Amheklerior/unity-core-library#Features
[Getting Started]: https://github.com/Amheklerior/unity-core-library#Getting-Started
[Requirements]: https://github.com/Amheklerior/unity-core-library#Requirements
[Roadmap]: https://github.com/Amheklerior/unity-core-library#Roadmap
[Support]: https://github.com/Amheklerior/unity-core-library#Support
[Licence]: https://github.com/Amheklerior/unity-core-library#Licence



## Features
 - [Event System][]
 - [Object Pooling][]
  
[Event System]: https://github.com/Amheklerior/unity-core-library/blob/develop/Documentation/Event-System.md 
[Object Pooling]: https://github.com/Amheklerior/unity-core-library/blob/develop/Documentation/Object-Pooling.md 

<!-- TODO: change the link after merging into master -->



## Getting Started
To be ready to use this tools, you simply have to install the package in your Unity project. 
You can import the package either [using the Unity Package Manager (UPM)][] or [manually][].  

[using the Unity Package Manager (UPM)]: https://github.com/Amheklerior/unity-core-library#Import-the-Package-Using-the-UPM  
[manually]: https://github.com/Amheklerior/unity-core-library#Manually-Import-the-Package


### Import the Package Using the UPM
To import the package via the Unity Package Manager using the Git URL, you can simply follow the instructions described 
in the [Installing from a Git URL][] Unity manual page.

[Installing from a Git URL]: https://docs.unity3d.com/Manual/upm-ui-giturl.html


### Manually Import the Package 
To manually install the package, follow these steps:  

1. Download the repository and put the files in a folder named __com.amheklerior.core-library__ in your Packages folder  

2. Edit your __manifest.json__ file in your Packages folder and add the following line as a dependency: 

		"com.amheklerior.core-library": "file:com.amheklerior.core-library" 

3. Follow the workflow described in the [Assembly Definitions][] Unity manual page, and add a reference 
   to __com.amheklerior.core-library.runtime.asmdef__

   [Assembly Definitions]: https://docs.unity3d.com/Manual/ScriptCompilationAssemblyDefinitionFiles.html  



## Requirements
This package has been developed using _Unity.2019.3.10f1_ and, 
although it has not been designed for any specific version of Unity, 
some tools might REQUIRE features from this version of Unity.  

Therefore, although there are no specific requirements, 
it is suggested  to use the Unity.2019 or higher version of the engine.  



## Development & Roadmap
WIP         



## Support
If you need support, find any issue you'd like to report, or have any suggestions for improvement, 
please visit the [Issue-Tracker][]

[Issue-Tracker]: https://github.com/Amheklerior/unity-core-library/issues  
   


## Licence
This package is licenced under the [MIT Licence][MIT Licence]

[MIT Licence]: https://github.com/Amheklerior/unity-core-library/blob/develop/LICENSE  

<!-- TODO: change the link after merging into master -->

