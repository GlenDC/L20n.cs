# L20n.cs

An implementation of L20n-For-Games in C#, Targeting the Unity Game Engine.

The grammar specs for the "L20n-For-Games" language can be found [here](https://github.com/GlenDC/L20n/blob/master/design/l20n-specs.md).

## Contributing

Contributions are always welcome.
Ideas, suggestions, Issue Reports are just as good and welcome as code contributions.
The library is finished, meaning that it contains all the functionality that was aimed for,
but that doesn't mean there is no room for improvement. Just keep in mind that the aim
is to keep the library and L20n language as simple as possible,
meaning that we should minimize the amount the user of the language has to learn.

Do you want to contribute, but you have no idea where you can make yourself useful?
Here are some ideas:

+ The entire codebase contains documentation, both inline and as XML comments. Writing good documentation however, is hard.
  There is definitely room for improvement here, so feel free to correct my English (not my native language) and
  modify the existing documentation to make it more clear, or add missing information.
+ There are lot of unit test and I tried to apply the TDD principle as much as possible.
  Do you see any missing unit-tests? Feel free to add them.
+ This lib can be used in any .NET 2+ environment, but is specifically written to be used in a commercial Unity package.
  Unity supports a lot of platforms, and some of them really need performant code.
  Localization isn't that important compared to other areas and should therefore be as invisible as possible,
  in terms of performance. I'm 99% sure there is room for improvement in this area, so if you're good
  at writing performant C# code, feel free to contribute and others will be grateful for it.
+ It might be useful to be able to transpile the L20n to C# byte code, so that this can be loaded at initialization.
  This way the entire parsing/AST code can be skipped in production code. I'm not sure if this is really something
  we want to do, as the parsing time seems neglectable, but it's an idea and feel free to work on it if
  you feel like it.

Some guidelines for contributions:

+ Please respect the coding standards already present in the code base.
  I'm not a fan of these coding standards, but I do aim for consistency.
+ When adding new "special cases" or features, or any other modification,
  make sure to add unit-tests to reflect those changes. Never remove existing test cases
  unless you have a very good reason to do so.
+ In order to submit your contribution, simply fork this repository and make a pull request.

## Credits

+ [GlenDC](https://github.com/GlenDC) (developer/maintainer);

## License

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use the content of this repository except
    in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
