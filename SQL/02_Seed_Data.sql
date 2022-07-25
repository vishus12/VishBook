USE [VishBook]
GO

set identity_insert [UserProfile] on
insert into [UserProfile] ([Id] , [FirebaseUserId] , [Email])
VALUES  (1, '8hsauMNgFNfSv9pcYNlYrhzDiMA2', 'al@al.com'),
		(2, 'KTxhvZ702VhtA2zdmNo324W8WF92', 'bb@bb.com')
set identity_insert [UserProfile] off

set identity_insert [Post] on
insert into [Post] ([Id] , [Title] , [Content] , [CreateDateTime] , [UserId])
VALUES (1, 'WorD', 'Goodbye There' , '2022-07-20', 1),
	   (2, 'Bird', 'Hello There' , '2022-07-20', 2)
set identity_insert [Post] off

set identity_insert [Mood] on
insert into [Mood] ([Id], [Name])
VALUES (1, 'Love'),
	   (2, 'Hate')
set identity_insert [Mood] off

set identity_insert [PostMood] on
insert into [PostMood] ([Id], [PostId], [MoodId])
VALUES (1, 1, 1),
	   (2, 2, 2)
set identity_insert [PostMood] off