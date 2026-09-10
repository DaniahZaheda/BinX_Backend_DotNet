# Sprint 3 – Performance Retrospective & Summary

## Sprint Review

Sprint 3 focused on improving API performance through query optimization, Redis caching, and database indexing.

### Performance Evidence

| Area                 |                  Before |          After |
| -------------------- | ----------------------: | -------------: |
| Redis response time  |                 1836 ms |     **336 ms** |
| Index logical reads  |                      54 |          **9** |
| Index execution plan | Index Seek + Key Lookup | **Index Seek** |

The Redis cache improved response time significantly, while the covering index reduced logical reads by approximately **83%** and eliminated the Key Lookup.

Query optimization was also implemented using eager loading and projection to reduce unnecessary database queries and returned data.

## Sprint Review Result

All planned Sprint 3 performance tasks were completed and validated using measurable evidence.

* Query optimization implemented.
* Redis caching implemented with cache invalidation.
* Composite covering index implemented.
* Performance measured using SQL Server execution plans and statistics.
* Test data was removed after performance testing.

## Sprint 4 Backlog

### Performance Regression Tests

Add automated tests to verify important endpoints keep their expected query count and prevent future N+1 query regressions.

## Retrospective

### What Went Well

* Performance improvements were measured using real before/after results.
* SQL Server execution plans helped identify the effect of indexing.
* Redis caching noticeably improved response time.
* The covering index removed the unnecessary Key Lookup.

### What Could Be Improved

* Performance testing could be automated instead of relying mainly on manual checks.
* More realistic datasets could be used for future benchmarking.
* Performance requirements should be considered earlier when designing endpoints.

### Action for Sprint 4

**Add automated performance regression tests for important API endpoints, especially query-count checks, to prevent performance regressions from being reintroduced.**
