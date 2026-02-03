# FunBooksAndVideos E-commerce Shop

A technical prototype of an e-commerce order processing system for a shop offering books, videos, and club memberships. This project demonstrates clean code principles, design patterns, and the latest features of .NET 10.

## 🚀 Tech Stack

- **Runtime:** .NET 10
- **Language:** C#
- **Architecture:** Clean Architecture (Domain, Application, Infrastructure, API)
- **Key Features:** Primary Constructors, Pattern Matching, Records
- **Testing:** xUnit, Moq/NSubstitute, FluentAssertions

## 🛠 Business Rules

The system implements the following requirements during purchase order processing:

- **BR1 (Membership Activation):** If the purchase order contains a membership, it must be activated in the customer account immediately.
- **BR2 (Physical Shipping):** If the purchase order contains a physical product, a shipping slip must be generated.

## 🏗 Project Structure

- `FunBooksAndVideos.Domain`: Core entities, value objects, and domain logic.
- `FunBooksAndVideos.Application`: Use cases and order processing logic (`OrderProcessor`).
- `FunBooksAndVideos.Api`: RESTful endpoints for system interaction.
- `FunBooksAndVideos.Tests`: Comprehensive unit and integration tests.

## 🚦 Getting Started

1. Ensure you have the **.NET 10 SDK** installed.
2. Clone the repository.
3. Run the following commands in the root directory:
   ```bash
   dotnet restore
   dotnet build
   dotnet test