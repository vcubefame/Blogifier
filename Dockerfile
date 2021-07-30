FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine as base

# download seeker agent from the server
RUN curl -v -k -o seeker-agent.zip "https://server01.seekerpoc.synopsys.com/rest/api/latest/installers/agents/binaries/DOTNETCORE?osFamily=LINUX"
# extract to /tmp/seeker
RUN mkdir -p /tmp/seeker && unzip -d /tmp/seeker seeker-agent.zip
RUN export CORECLR_ENABLE_PROFILING=1
RUN export CORECLR_PROFILER_PATH=/tmp/seeker/x64/Agent.Profiler-Dnc.so
RUN export CORECLR_PROFILER={C7AD49B3-093B-4AB1-A241-1E4365DD8901}
RUN export SEEKER_DNC_INSTALL=/tmp/seeker
RUN export SEEKER_SERVER_URL="https://server01.seekerpoc.synopsys.com"
RUN export SEEKER_PROJECT_KEY="varuneshop"

# Copy everything else and build
COPY ./ /opt/blogifier
WORKDIR /opt/blogifier

RUN ["dotnet","publish","./src/Blogifier/Blogifier.csproj","-o","./outputs" ]

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine as run
COPY --from=base /opt/blogifier/outputs /opt/blogifier/outputs
WORKDIR /opt/blogifier/outputs
ENTRYPOINT ["dotnet", "Blogifier.dll"]
