all: clean format build tests
	make packages -B
	make docfx -B
	make sdk -B
	make swagger -B
	make migration
	make angular -B

format:
	- dotnet format

clean:
	-dotnet clean
	-rimraf docs
	-rimraf packages
	-rimraf coverage.xml
	

build: src samples

src:
	-dotnet build ./Falcon.sln --no-restore --no-incremental

samples:
	-dotnet build ./samples/samples.sln

tests:
	-dotnet test ./tests/Falcon.Tests.sln

test-result-1:
	-dotnet test ./tests/Falcon.Test.sln --collect:"XPlat Code Coverage" --results-directory "TestResults"
	-reportgenerator -reports:"TestResults\{guid}\coverage.cobertura.xml" -targetdir:"TestResults" -reporttypes:Html

test-result-2:
	-dotnet add package coverlet.msbuild
	-dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=lcov.info --results-directory "TestResults"
	-reportgenerator -reports:"TestResults\lcov.info" -targetdir:"TestResults" -reporttypes:Html

packages:
	-dotnet pack -c release  -o ./packages

docfx:
	-docfx docfx/docfx.json

docs: 
	-docfx docfx/docfx.json --serve

outdated:
	-dotnet outdated

deprecated:
	-dotnet list package --deprecated

nuget-clear:
	-dotnet nuget locals all --clear

swagger:
	-swagger tofile --output open-api.json ./samples/Api.Module.Sample/bin/Debug/net9.0/Api.Module.Sample.dll v1

angular:
	-npx @openapitools/openapi-generator-cli generate -i open-api.json -g typescript-angular -o client/angular
	-npx @openapitools/openapi-generator-cli generate -i open-api.json -g dotnet -o client/dotnet

run-sample:
	-setx ASPNETCORE_ENVIRONMENT "Development"
	-dotnet run --project ./samples/Api.Module.Sample/Api.Module.Sample.csproj

run:
	-setx ASPNETCORE_ENVIRONMENT "Development"
	-dotnet run --project ./src/Falcon.Hosts/Falcon.Hosts.csproj

sdk:
	setx ASPNETCORE_ENVIRONMENT "Development"
	dotnet run --project ./sdk/ClientSdkGenerator/ClientSdkGenerator.csproj	

migration:
	dotnet ef migrations add InitialCreate --startup-project ./sdk/ClientSdkGenerator --project ./src/Falcon.DesignTimeMigration --context HomeDbMigrationContext

update:
	dotnet tool list -g
	dotnet tool update -g coverlet.console                
	dotnet tool update -g docfx                           
	dotnet tool update -g dotnet-counters                 
	dotnet tool update -g dotnet-coverage                 
	dotnet tool update -g dotnet-doc                      
	dotnet tool update -g dotnet-ef                       
	dotnet tool update -g dotnet-format                   
	dotnet tool update -g dotnet-monitor                  
	dotnet tool update -g dotnet-sonarscanner             
	dotnet tool update -g dotnet-version-cli
	dotnet tool update -g swashbuckle.aspnetcore.cli
	dotnet tool update -g versionize
	dotnet tool update -g dotnet-outdated-tool
	dotnet tool update -g nbgv
	dotnet tool update -g dotnet-reportgenerator-globaltool
	dotnet tool update -g husky
	dotnet tool update -g csharpier
	dotnet tool list -g

install:
	dotnet tool list -g
	-dotnet tool install -g	coverlet.console
	-dotnet tool install -g	docfx
	-dotnet tool install -g	dotnet-counters
	-dotnet tool install -g	dotnet-coverage
	-dotnet tool install -g	dotnet-doc
	-dotnet tool install -g	dotnet-ef
	-dotnet tool install -g	dotnet-format
	-dotnet tool install -g	dotnet-monitor
	-dotnet tool install -g	dotnet-sonarscanner
	-dotnet tool install -g	dotnet-version-cli
	-dotnet tool install -g	swashbuckle.aspnetcore.cli
	-dotnet tool install -g	versionize
	-dotnet tool install -g	dotnet-outdated-tool
	-dotnet tool install -g	nbgv
	-dotnet tool install -g dotnet-reportgenerator-globaltool
	-dotnet tool install -g husky
	-dotnet tool install -g csharpier
	dotnet tool list -g

setup: install
	-npm install -g rimraf
	-npm install -g cspell

host:
	dotnet build ./src/Falcon.Hosts/Falcon.Hosts.csproj

coverage:
	-dotnet test .\library\Falcon.QueryBuilder.Test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov /p:CoverletOutput=lcov.info
	-reportgenerator -reports:".\library\Falcon.QueryBuilder.Test\lcov.info" -targetdir:"TestResults" -reporttypes:Html