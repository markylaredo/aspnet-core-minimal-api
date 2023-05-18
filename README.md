# ASP Net Core 6 Minimal API

# Features
- Lightweight and fast API implementation using ASP.NET Core 6 Minimal API.
- Interact with student data, including retrieval, creation, update, and deletion of student records.
- Supports CRUD operations (Create, Read, Update, Delete) for student records.
- Uses JSON format for data exchange.
- Simple and intuitive endpoint structure for easy integration and usage.


# API Endpoint
This API endpoint allows you to interact with student data, including retrieving, creating, updating, and deleting student records.

## Request
### HTTP Method
- GET: Retrieve a list of all students
- POST: Create a new student record
- PUT: Update an existing student record
- DELETE: Delete a student record

### Request URL
- GET: `http://minimal-api-demo.somee.com/students`
- POST: `http://minimal-api-demo.somee.com/students`
- PUT: `http://minimal-api-demo.somee.com/students/{id}`
- DELETE: `http://minimal-api-demo.somee.com/students/{id}`

### Request Body
- POST: Include the following JSON object in the request body:
```json
{
  "id": "ac778e22-aa86-40a9-83ff-b334239cdf2e",
  "name": "Jazelle Laredo",
  "level": "4",
  "section": "A",
  "academicYear": "2013-2014"
}

```

### Response Body
- POST: The response body for the HTTP POST request will contain the created student record in JSON format:
```json
{
  "id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "name": "Mark Anthony",
  "level": "4",
  "section": "C",
  "academicYear": "2014-2015"
}

```
