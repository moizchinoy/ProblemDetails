
[Handle errors in ASP.NET Core controller-based web APIs](https://learn.microsoft.com/en-us/aspnet/core/web-api/handle-errors?view=aspnetcore-7.0)

In `Program.cs`, `builder.Services.AddProblemDetails()` affects the output of unhandled exceptions and situations where an unsuccessful response is sent without a body.

Unhandled exceptions are captured by `app.UseExceptionHandler()`, which requires `builder.Services.AddProblemDetails()` to function properly.

Unsuccessful responses without a body are handled by `app.UseStatusCodePages()`. Without `builder.Services.AddProblemDetails()`, it returns `text/plain`; with it, it returns `application/problem+json`.

```
curl.exe -i -X 'GET' 'http://localhost:5220/problemdetails/badrequest'

HTTP/1.1 400 Bad Request
Content-Type: application/problem+json; charset=utf-8
Date: Tue, 07 Jan 2025 14:13:38 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
    "title": "Bad Request",
    "status": 400,
    "traceId": "00-29d62e31b7eafd5664bff4d4cafc0378-0110db8baa6d6bf2-00"
}
```

```
curl.exe -i -X 'GET' 'http://localhost:5220/problemdetails/notimplementedexception'

HTTP/1.1 500 Internal Server Error
Content-Type: application/problem+json; charset=utf-8
Date: Tue, 07 Jan 2025 14:14:13 GMT
Server: Kestrel
Cache-Control: no-cache,no-store
Expires: -1
Pragma: no-cache
Transfer-Encoding: chunked

{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.6.1",
    "title": "An error occurred while processing your request.",
    "status": 500,
    "traceId": "00-f5463d2e24a6913c91133b8dce735625-2c73264aadcc27e8-00"
}
```

```
curl.exe -i -X 'GET' 'http://localhost:5220/problemdetails/notfound'

HTTP/1.1 404 Not Found
Content-Type: application/problem+json; charset=utf-8
Date: Tue, 07 Jan 2025 14:14:45 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-5f83c9dfe2d4c1a75fcf1fd5b606e2d7-3ef0048837b735fb-00"
}
```

```
curl.exe -i -X 'GET' 'http://localhost:5220/problemdetails/404'

HTTP/1.1 404 Not Found
Content-Type: application/problem+json; charset=utf-8
Date: Tue, 07 Jan 2025 14:16:10 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
    "title": "Not Found",
    "status": 404,
    "traceId": "00-b235fbc9d4d30bef566d3297c5aaafad-c9bd5cbf782b3d07-00"
}
```

```
curl.exe -i -X 'GET' 'http://localhost:5220/problemdetails/custombadrequest'

HTTP/1.1 400 Bad Request
Content-Type: application/json; charset=utf-8
Date: Tue, 07 Jan 2025 14:16:47 GMT
Server: Kestrel
Transfer-Encoding: chunked

{
    "a": 1,
    "b": "ff"
}
```