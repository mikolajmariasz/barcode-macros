# Meal Management Application

## Project Overview

### Core Functionalities:
- **Adding new meals**:
  - Entering meal names
  - Adding ingredients via product PLU codes
  - Specifying ingredient weights
  - Automatic product data retrieval from external API

- **Browsing meals**:
  - Displaying all saved meals
  - Viewing ingredients for each meal
  - Automatic calculation of total nutritional values based on ingredients

- **Deleting meals**:
  - Deleting existing meals from the database

- **API Integration**:
  - Fetching food product data from Open Food Facts using PLU codes

- **Database Operations**:
  - Storage using Entity Framework Core
  - Relationship between meals and ingredients

### API Integration Details
The application integrates with the Open Food Facts API to retrieve product information. This functionality is implemented in the `APIService` class.
- **PLU Code Lookup**: Fetches product data using PLU codes
- **Nutritional Data Parsing**: Extracts nutritional information
- **Error Handling**: handles API errors and missing data

### Database Structure
This application uses relational database with four main entities, implemented using SQLite and Entity Framework Core:
  
#### `Meals`
- `Id` (PK)
- `MealName`

#### `Ingredients`
- `Id` (PK)
- `WeightInGrams`
- `MealId` (FK to Meals)
- `ProductId` (FK to FoodProducts)

#### `FoodProducts`
- `Id` (PK)
- `Code` (barcode)
- `Name`
- `EnergyKcal` (per 100g)
- `Proteins` (per 100g)
- `Carbohydrates` (per 100g)
- `Fat` (per 100g)
- `Fiber` (per 100g)
- `Salt` (per 100g)

#### Relationships

1. **Meal → Ingredients** (1-to-many)
   - One meal has many ingredients

2. **Ingredient → FoodProduct** (many-to-1)
   - Many ingredients reference one product

### GUI
This application has GUI implemented using MAUI framework, with two pages:
1. MainPage (browsing meals)
<img width="800" alt="barcodes_macros_1" src="https://github.com/user-attachments/assets/488fb838-3931-472d-b3c0-dd42bc9c5296" />

2. AddMealPage (adding meal)
<img width="800" alt="barcodes_macros_2" src="https://github.com/user-attachments/assets/e0e83470-cc53-4334-b9f5-c673c7631be4" />


