## Day 2 — Query Optimization with Eager & Explicit Loading

Today, I practiced EF Core query optimization using eager loading and projection. I used `Include` to load project owners in a single SQL query and compared it with projection using `Select`. Both approaches executed one SQL query, but projection selected only the required columns, making it more lightweight.

### Before / After

* **Before:** Loading related data can lead to unnecessary database queries or excessive data loading.
* **After:** `Include` loads related data in one query, while projection retrieves only the required fields.

### Results

* `Include`: 1 SQL query, approximately **4 ms** database execution time.
* `Projection`: 1 SQL query, approximately **1 ms** database execution time.
* Projection returned only the required project and owner fields.
* No N+1 query pattern was observed in the tested endpoints.

### Conclusion

`Include` is useful when the related entity is needed, while projection is preferable when only specific fields are required because it reduces the amount of data retrieved from the database.
