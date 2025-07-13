# StealAllTheCats

The implemented web api consists of five endpoints:

1. /api/cats/fetch 
		Fetches 25 cats from the cat API (https://thecatapi.com/) and saves them in the database.
		The request is executed as background job through HangFire. It returns the job id.
2.	/api/cats/{id}
		Fetches a cat from the database for the given id.
3.	/api/cats?page=1&pagesize=10
		Fetches the cats from the database with pagination. The query parameters "page" and "pagesize" are required.
4. 	/api/cats?page={page}&pagesize={pagesize}&tag={tag}
		Fetches the cats from the database with pagination and tag filtering. The query parameters "page" and "pagesize" are required.
		The query parameter "tag" is optional.
5. 	/api/jobs/{jobid}
		Fetches the job for the given job id.
		
SQL Server Express is used as database storage and Entity Framework as ORM.
For the background jobs is HangFire chosen. For the mappping between entity, dto and http client models is Automapper used.


