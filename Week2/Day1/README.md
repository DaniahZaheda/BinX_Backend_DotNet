# Day 1 - Generics & Advanced Collections

## Learning Objectives

* Understand why Generics are used.
* Create generic classes and methods.
* Apply generic constraints using `where`.
* Learn when to use different collection interfaces.

## What I Built

For this lab, I created a simple generic `Repository<T>` class that includes:

* `Add()` to add items.
* `GetAll()` to return all items as `IReadOnlyList<T>`.
* `Find()` to search for an item using a predicate.

I also used the repository with two different classes (`Student` and `Course`) and applied the `where T : class` constraint.

## What I Learned

* Generics make code reusable and type-safe.
* Generic constraints limit the types that can be used.
* `IReadOnlyList<T>` is useful when data should only be read and not modified.
