# FIAB
> Success has many fathers, but **f**ailure **i**s **a** **b**astard.

This project is a simple tool to model and manage *relationships* between *nouns*.

## Run Using Docker
You will need a Postgres database running, with an empty database for FIAB to use, and a connection string for that database for a user with permissions that can `CREATE` and `DROP` tables.  An example connection string can be found in `secrets.example.json`.

FIAB will use Entity Framework migrations to scaffold the database when it launches.

Build and Tag a docker image in the root of the repository run
```bash
docker build -f Dockerfile -t fiab:0.0.1-alpha .
```
Command breakdown
* `docker build` - tells docker to build a new image
* `-f Dockerfile` - build the image using `Dockerfile` in the root of the repository
* `-t fiab` - name the image it produces "fiab"
* `:0.0.1-alpha` - the version you would like to "tag" your image as
* `.` - instructs docker to build using the contents of the current folder.  Very easy to forget.


Once the image builds you can launch a container using it by running
```bash
docker run -d -p 8080:80 -e ConnectionString="Server=127.0.0.1;Port=5432;Database=fiab-demo-db;User Id=fiabSA;Password=my_password;" --name fiab-demo fiab:0.0.1-alpha
```
Command breakddown
* `docker run` - tells docker to run a new container
* `-d` - start the new container in a "detatched" state
* `-p 8080:80` - Listen for requests on port `8080` of localhost, and that traffic goes to port `80` inside the container
* `-e ConnectionString="Server=127.0.0.1;Port=5432;Database=fiab-demo-db;User Id=fiabSA;Password=my_password;"` - provide the connection string to the database as an environment variable in the container
	* If you want to use email confirmation and password reset emails you'll need to add the following environment variables
		* `-e SmtpServer="smtp.yourdomain.com"` - the hostname of your SMTP server
		* `-e SmtpPort=465` - The port used for ["explicit"](https://stackoverflow.com/a/53888031) SSL requests
		* `-e SmtpUser="youremail@yourdomain.com"`
		* `-e SmptPassword="yourSmtpPassword"`
* `--name fiab-demo` - Name the new container `fiab-demo`
* `fiab:0.0.1-alpha` - the name of the image we built previously

You should now be able to open a browser to `http://localhost:8080/` and see the landing page for FIAB.

If you the page does not load you may want to run
```bash
docker logs fiab-demo
```
To examine any errors that may have prevented your container from starting or handling your request.