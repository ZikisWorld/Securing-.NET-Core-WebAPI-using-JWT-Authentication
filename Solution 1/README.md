
This repo contains a solution to secure .NET core API using JWT authentication

Solution 1:
In this solution, JWT Token generation and validation in the same Application
  1. Client calls API Login Method. On Successful Login, Token is generated and sent as reponse to the client.
  2. Client attaches the bearer token while sending subsequent requests to API
  3. Token is validated by the API. On succesful token validation, the request is processed by the API