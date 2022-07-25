USE [master]

IF db_id('VishBook') IS NULl
  CREATE DATABASE [VishBook]
GO

USE [VishBook]
GO


DROP TABLE IF EXISTS [PostMood];
DROP TABLE IF EXISTS [Mood];
DROP TABLE IF EXISTS [Post];
DROP TABLE IF EXISTS [UserProfile];
GO

CREATE TABLE [UserProfile] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Email] nvarchar(255) UNIQUE NOT NULL,
  [FirebaseUserId] nvarchar(255) UNIQUE NOT NULL
)
GO

CREATE TABLE [Post] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Title] nvarchar(255) NOT NULL,
  [Content] nvarchar(255),
  [CreateDateTime] datetime NOT NULL,
  [UserId] int NOT NULL
)
GO

CREATE TABLE [Mood] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [Name] nvarchar(255) NOT NULL
)
GO

CREATE TABLE [PostMood] (
  [Id] int PRIMARY KEY IDENTITY(1, 1),
  [PostId] int NOT NULL,
  [MoodId] int NOT NULL
)
GO

ALTER TABLE [Post] ADD FOREIGN KEY ([UserId]) REFERENCES [UserProfile] ([Id]) ON DELETE CASCADE
GO

ALTER TABLE [PostMood] ADD FOREIGN KEY ([MoodId]) REFERENCES [Mood] ([Id])
GO

ALTER TABLE [PostMood] ADD FOREIGN KEY ([PostId]) REFERENCES [Post] ([Id]) ON DELETE CASCADE
GO
