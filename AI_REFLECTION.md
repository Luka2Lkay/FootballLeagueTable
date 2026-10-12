I used GitHub Copilot to help me write the code for the Football League Table project. It was able to generate code snippets based on my prompts and suggestions, which saved me a lot of time. 
However, I had to review and modify the generated code to ensure it met my requirements and followed best practices.

One issue I encountered was that some of the generated code was not optimal or did not handle edge cases, so I had to refactor it. For example, Copilot suggested a test called `Name_PreservesProvidedWhitespace`, which expected a team name such as `"  Liverpool  "` to retain the leading and trailing spaces. 
Although this test matched the behaviour of the original code, I realised that preserving unnecessary whitespace could lead to inconsistent team names in the league table. I had to reconsider the test and whether team names should be trimmed to ensure consistency.

Copilot also focused more on getting the main functionality working than handling every possible scenario. I had to look more closely at how the program handled CSV input and what would happen if a row contained invalid values. 
I used `int.TryParse` to check whether score values were valid integers, and I added error handling to skip invalid rows and log a warning message. This made the program more robust and user-friendly.

The experience reminded me that AI-generated code is a starting point, and I need to apply my own judgment to ensure the final product is reliable and maintainable. 
Overall, I found the AI assistance helpful, but it reinforced the importance of understanding the code, questioning generated suggestions, and adapting them to meet the specific requirements of the project.