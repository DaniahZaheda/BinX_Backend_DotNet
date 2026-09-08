## Day 3 – Redis Caching

Implemented Redis distributed caching using ASP.NET Core `IDistributedCache` and the Cache-Aside Pattern.

The `GET /api/projects` endpoint:

* Checks Redis before querying the database.
* Stores database results in Redis for 10 minutes.
* Uses cache keys based on pagination and filtering parameters.
* Invalidates the cache after Create, Update, or Delete operations.

### Performance Testing

| Request    |    Time |
| ---------- | ------: |
| Cache Miss | 1836 ms |
| Cache Hit  |  336 ms |

The cached request was approximately **5.46× faster**, reducing response time by **81.7%**.

Testing also confirmed that after updating a project, the next GET request returns the updated data with a **Cache Miss**, followed by a **Cache Hit** on subsequent requests.
