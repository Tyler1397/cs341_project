SELECT M.Id,M.Message from dbo.Messages M join dbo.AllUsers A on M.Username = A.Username where M.Username='TylerTimm'
