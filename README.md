# Taxually technical test - About my solution

I have enhanced the code to follow the SOLID principles. To do that, I have:
- Refactored the code to use the Factory pattern for VAT registration creation
- Add dependency injection for new services
- Add interfaces for already existing code clients too, and apply dependency injection on them too.
  
- Also, add tests with a new test project (as requested)

- Use enums for countries (just to make the code more readable and to avoid too many hardcoded parts)
- Add const for filenames (just to make the code more readable, thinking there will be a need for these later too, avoiding multiple hardcoded names)


# Taxually technical test - original assignment

This solution contains an [API endpoint](https://github.com/Taxually/developer-test/blob/main/Taxually.TechnicalTest/Taxually.TechnicalTest/Controllers/VatRegistrationController.cs) to register a company for a VAT number. Different approaches are required based on the country where the company is based:

- UK companies can register via an API
- French companies must upload a CSV file
- German companies must upload an XML document

We'd like you to refactor the existing solution with the following in mind:

- Readability
- Testability
- Adherance to SOLID principles

We'd also like you to add some tests to show us how you'd test your solution, although we aren't expecting exhaustive test coverage.

We don't expect you to implement the classes for making HTTP calls and putting messages on queues.

We'd like you to spend not more than a few hours on the exercise.

To develop and submit your solution please follow these steps:

1. Create a public repo in your own GitHub account and push the technical test there
2. Develop your solution and push your changes to your own public GitHub repo
3. Once you're happy with your solution send us a link to your repo
