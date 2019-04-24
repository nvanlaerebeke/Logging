ROOT:=$(shell pwd)
PROJECT=Logging
BUILDDIR=build

CONFIGURATION=Debug
VERSION=$(shell cat VERSION)
REVISION=$(shell git rev-parse --short HEAD)

build: clean app

clean:
	echo $(ROOT)
	rm -rf $(ROOT)/build
	rm -rf $(ROOT)/*/bin/
	rm -rf $(ROOT)/*/obj/
	#rm -rf packages

app:
	#nuget restore $(ROOT)/src/$(PROJECT).sln
	#nuget restore $(ROOT)/src/$(PROJECT).*.sln
	
	msbuild $(ROOT)/src/$(PROJECT).sln /t:$(PROJECT) /p:Configuration="$(CONFIGURATION)" /p:Platform="Any CPU" 
	msbuild $(ROOT)/src/$(PROJECT).*.sln /p:Configuration="$(CONFIGURATION)" /p:Platform="Any CPU"
