# Contributing to Inventory Management System

Thank you for your interest in contributing to this project! This guide will help you get started.

## 🛠️ Development Setup

### Prerequisites
- .NET 8 SDK or later
- Node.js 18+ and npm 11.6.2+
- Angular CLI 21+
- Git
- Visual Studio Code (recommended) or Visual Studio 2022

### Getting Started

1. **Fork and Clone**
   ```bash
   git clone https://github.com/YOUR_USERNAME/InventorySystem_Hackathon.git
   cd InventorySystem_Hackathon
   ```

2. **Backend Setup**
   ```bash
   cd InventorySystem/InventorySystem.Api
   dotnet restore
   dotnet build
   dotnet run
   ```

3. **Frontend Setup**
   ```bash
   cd InventorySystem.Client
   npm install
   ng serve
   ```

## 📋 Project Structure

```
InventorySystem/
├── InventorySystem.Api/          # Web API Controllers & Startup
├── InventorySystem.Application/  # Service Interfaces (Business Logic Contracts)
├── InventorySystem.Core/         # Domain Models (Entities)
├── InventorySystem1.Infrastructure/ # Data Access (EF Core, DbContext)
├── InventorySystem.Tests/        # Unit Tests (XUnit)
└── InventorySystem.Client/       # Angular Frontend
```

## 🎯 How to Contribute

### Reporting Bugs
1. Check if the bug is already reported in [Issues](https://github.com/Konark1/InventorySystem_Hackathon/issues)
2. If not, create a new issue with:
   - Clear title and description
   - Steps to reproduce
   - Expected vs actual behavior
   - Screenshots (if applicable)
   - Environment details (OS, .NET version, browser)

### Suggesting Features
1. Open a new issue with the label `enhancement`
2. Describe the feature and its benefits
3. Provide use cases and examples

### Pull Requests

1. **Create a Branch**
   ```bash
   git checkout -b feature/your-feature-name
   # or
   git checkout -b fix/bug-description
   ```

2. **Make Your Changes**
   - Follow the existing code style
   - Write clean, readable code
   - Add comments for complex logic
   - Update documentation if needed

3. **Test Your Changes**
   ```bash
   # Backend tests
   cd InventorySystem/InventorySystem.Tests
   dotnet test
   
   # Frontend tests
   cd InventorySystem.Client
   npm test
   ```

4. **Commit Your Changes**
   ```bash
   git add .
   git commit -m "feat: add new feature description"
   # or
   git commit -m "fix: resolve bug description"
   ```

5. **Push and Create PR**
   ```bash
   git push origin feature/your-feature-name
   ```
   Then open a Pull Request on GitHub with:
   - Clear description of changes
   - Related issue numbers
   - Screenshots (for UI changes)

## 📝 Coding Standards

### Backend (.NET/C#)
- Follow C# coding conventions
- Use meaningful variable and method names
- Keep methods small and focused (Single Responsibility)
- Add XML documentation comments for public APIs
- Use async/await for database operations
- Handle exceptions appropriately

Example:
```csharp
/// <summary>
/// Retrieves all inventory items from the database.
/// </summary>
/// <returns>A list of inventory items.</returns>
public async Task<IEnumerable<InventoryItem>> GetAllItemsAsync()
{
    return await _context.Items.ToListAsync();
}
```

### Frontend (Angular/TypeScript)
- Follow Angular style guide
- Use TypeScript types (avoid `any`)
- Create reusable components
- Use reactive programming (RxJS) for async operations
- Follow component naming: `feature-name.component.ts`
- Add proper error handling

Example:
```typescript
export interface InventoryItem {
  id: number;
  name: string;
  quantity: number;
  lowStockThreshold: number;
}
```

## 🧪 Testing Guidelines

### Backend Tests (XUnit)
- Write tests for all service methods
- Use meaningful test names: `MethodName_Scenario_ExpectedResult`
- Arrange-Act-Assert pattern
- Mock external dependencies

Example:
```csharp
[Fact]
public async Task GetAllItemsAsync_ReturnsAllItems_WhenItemsExist()
{
    // Arrange
    var mockContext = CreateMockContext();
    
    // Act
    var result = await _service.GetAllItemsAsync();
    
    // Assert
    Assert.NotEmpty(result);
}
```

### Frontend Tests (Vitest)
- Test components, services, and pipes
- Mock HTTP calls
- Test user interactions

## 🔄 Commit Message Convention

Use conventional commits:
- `feat:` - New feature
- `fix:` - Bug fix
- `docs:` - Documentation changes
- `style:` - Code style changes (formatting, no logic change)
- `refactor:` - Code refactoring
- `test:` - Adding or updating tests
- `chore:` - Build process or auxiliary tool changes

Examples:
```
feat: add delete inventory item endpoint
fix: prevent negative stock quantities
docs: update API documentation
test: add unit tests for inventory service
```

## 🚀 Areas for Contribution

Here are some areas where contributions are welcome:

### High Priority
- [ ] Add UPDATE (PUT) endpoint for inventory items
- [ ] Add DELETE endpoint for inventory items
- [ ] Implement pagination for large datasets
- [ ] Add search and filter functionality
- [ ] Enhance error handling and validation

### Medium Priority
- [ ] Add JWT authentication
- [ ] Implement audit trail/history tracking
- [ ] Add export to CSV/Excel functionality
- [ ] Create dashboard with charts
- [ ] Add email notifications for low stock

### Low Priority
- [ ] Dark mode for frontend
- [ ] Multi-language support (i18n)
- [ ] Progressive Web App (PWA) features
- [ ] Performance optimizations
- [ ] Accessibility improvements (WCAG compliance)

## 📞 Getting Help

- Open an issue with the `question` label
- Contact the maintainer: [@Konark1](https://github.com/Konark1)

## 📄 License

By contributing, you agree that your contributions will be licensed under the same license as the project.

---

Thank you for contributing! 🎉
