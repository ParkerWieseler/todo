USE [ToDo]

GO

INSERT INTO Plans
([ID], [Name], [MaxContributors], [MaxLists], [CanNotifyViaEmail], [CanAddDueDates])
VALUES (1, 'Free', 0, 100, 0, 0)

INSERT INTO Plans
([ID], [Name], [MaxContributors], [MaxLists], [CanNotifyViaEmail], [CanAddDueDates])
VALUES (2, 'Basic', 5, 500, 1, 1)

INSERT INTO Plans
([ID], [Name], [MaxContributors], [MaxLists], [CanNotifyViaEmail], [CanAddDueDates])
VALUES (3, 'Premium', -1, -1, 1, 1)