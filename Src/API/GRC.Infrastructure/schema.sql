-- Users table
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Email TEXT NOT NULL UNIQUE,
    PasswordHash TEXT NOT NULL,
    OrganizationId INTEGER NOT NULL,
    RoleId INTEGER NOT NULL DEFAULT 1,
    Active INTEGER NOT NULL DEFAULT 0,
    CreatedAt TEXT NOT NULL,
    MobileNumber TEXT
);

-- Organizations table
CREATE TABLE IF NOT EXISTS Organizations (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    IsActive INTEGER NOT NULL DEFAULT 1,
    CreatedAt TEXT NOT NULL
);

-- Roles table
CREATE TABLE IF NOT EXISTS Roles (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT
);

-- Insert default role
INSERT OR IGNORE INTO Roles (Id, Name, Description) VALUES (1, 'User', 'Default user role');