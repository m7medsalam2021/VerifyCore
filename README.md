# Authentication System Usage Guide

## Endpoints

### 1. Register a New User

**Endpoint**: `POST /api/auth/register`

**Required Data**:
```json
{
  "username": "username",
  "email": "email",
  "password": "password"
}
Successful Request Example:

json
{
  "username": "ahmed",
  "email": "ahmed@example.com",
  "password": "A123456"
}
Success Response:
json
{
  "message": "User successfully registered",
  "userId": 1,
  "username": "ahmed"
}
2. Login
Endpoint: POST /api/auth/login

Required Data:

json
{
  "username": "username",
  "password": "password"
}
Successful Request Example:

json
{
  "username": "ahmed",
  "password": "A123456"
}
Success Response:

json
{
  "message": "Login successful",
  "userId": 1,
  "username": "ahmed",
  "isAdmin": false
}
How to Test
Using cURL
To register a new user:

bash
curl -X POST -H "Content-Type: application/json" -d '{"username":"test","email":"test@example.com","password":"Test123"}' http://localhost:5000/api/auth/register
To log in:

curl -X POST -H "Content-Type: application/json" -d '{"username":"test","password":"Test123"}' http://localhost:5000/api/auth/login
Using Postman
Create a new POST request

Set the URL to http://localhost:5000/api/auth/register or .../login

In the Headers section, add:

Key: Content-Type

Value: application/json

In the Body section, select raw and input the data as JSON

vbnet

Now you can copy everything above and paste it directly into your file! Let me know if you need anything else.