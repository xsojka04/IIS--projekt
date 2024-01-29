# School project VUT

The task of the project was to create an application for the creation of school tests, their completion, and evaluation in a 2-member team. We chose .NET because I had some experience with C#, I wanted to explore Razor Pages and my colleague wanted to tag along. 

We made this project using .NET CORE 3.1. We used the Visual Studio's template for Razor Pages. We used it, because it promised quick and easy implementation using C#. It was true. The implementation of the project was quite easy, but looking back I would rather spend my time learning JS frameworks such as React. To store data we used the MySQL database hosted on Endora.cz connected to our project using Entity Framework Core. We were having some problems with migrations and therefore we decided to not use them and code our database models ourselves. Authorization and authentication were a mandatory part of the project and we implemented it using Cookie Authentification scheme.
