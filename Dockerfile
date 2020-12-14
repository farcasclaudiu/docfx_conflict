FROM mono:latest as docfxbuild

VOLUME /src

WORKDIR /src

CMD mono /src/tools/docfx/docfx.exe "/src/docfx_project/docfx.json" --force --debug