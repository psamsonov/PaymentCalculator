# PaymentCalculator

## Running the project
Prerequisite: .NET Framework Core 3.1 or higher

Navigate to the project directory and run the following command: 
`dotnet run --project=PaymentCalculator`

Navigate your browser to `http://localhost:5000/report` to see the payments report.
Upload files by POSTing them to `http://localhost:5000/files` as form data.

```
curl --location --request POST 'http://localhost:5000/files' \
--form 'file=@time-report-2.csv'
```

## How did you test that your implementation was correct?

The project includes unit tests.

## If this application was destined for a production environment, what would you add or change?

* Real SSL and security on all of the endpoints
* Real database instead of an SQLite file
* Dependency injection
* More unit tests
* Integration tests
* Clean up serialization formatting and namespaces

## What compromises did you have to make as a result of the time constraints of this challenge?
* No dependency injection
* No security
* Unit tests are rather brief since dependency injection makes testing a lot easier
* 