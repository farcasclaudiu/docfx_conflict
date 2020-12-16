# docfx_conflict

for docfx version 2.56.6.0

Repro the issue with docfx when there is a naming clash between namespace and class - on macOS(Catalina) and docker container hosted on Windows or MacOS.

## Issue on macOS

```shell
docfx ./docfx_project/docfx.json --force --debug
```

Error:

```shell
[20-12-04 01:01:31.963]Error:[BuildCommand.BuildCore.Build Document.LinkPhaseHandlerWithIncremental.Apply Templates](api/docfx_conflict.yml)Error transforming model "/var/folders/87/y9_3c0qn76g9wlzzg5m8fsl40000gn/T/docfx/rawmodel/api/docfx_conflict.raw.json" generated from "api/docfx_conflict.yml" using "ManagedReference.html.primary.js". Error running Transform function inside template preprocessor: type is undefined
```

## Issue on Docker hosted on Windows or macOS

When running docfx in docker container (mono) and have mounted Windows or macOS volumes (see [Dockerfile](Dockerfile)).

```shell
docker build -t docfxtest .

// macOS or linux
docker run --name docfxtest -it --rm -v $(pwd):/src docfxtest:latest 
// or
// windows with powershell
docker run --name docfxtest -it --rm -v ${pwd}:/src docfxtest:latest
```

Error:
```shell
[20-12-14 09:18:39.424]Info:[MetadataCommand]Completed Scope:MetadataCommand in 3892.8232 milliseconds.
[20-12-14 09:18:39.425]Error:Error extracting metadata for /src/docfx_conflict/docfx_conflict.csproj: System.IO.IOException: Could not create file "/src/docfx_project/api/docfx_conflict.member.yml". File already exists.
  at System.IO.FileStream..ctor (System.String path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share, System.Int32 bufferSize, System.Boolean anonymous, System.IO.FileOptions options) [0x0019e] in <d13c8b563008422a8c5aaec0a74089cc>:0
  at System.IO.FileStream..ctor (System.String path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share, System.Int32 bufferSize) [0x00000] in <d13c8b563008422a8c5aaec0a74089cc>:0
  at (wrapper remoting-invoke-with-check) System.IO.FileStream..ctor(string,System.IO.FileMode,System.IO.FileAccess,System.IO.FileShare,int)
  at System.IO.File.Create (System.String path, System.Int32 bufferSize) [0x00000] in <d13c8b563008422a8c5aaec0a74089cc>:0
  at System.IO.File.Create (System.String path) [0x00000] in <d13c8b563008422a8c5aaec0a74089cc>:0
  at Microsoft.DocAsCode.Plugins.RootedFileAbstractLayer.Create (System.String file) [0x0001e] in <a2b39844b9794d2aa3575d5ba2d3e933>:0
  at Microsoft.DocAsCode.Plugins.FileAbstractLayerExtensions.CreateText (Microsoft.DocAsCode.Plugins.IFileAbstractLayer fal, System.String file) [0x00000] in <a2b39844b9794d2aa3575d5ba2d3e933>:0
  at Microsoft.DocAsCode.Common.YamlUtility.Serialize (System.String path, System.Object graph, System.String comments) [0x00005] in <0480e2a23e5145f69c3030fbeb298dc8>:0
  at Microsoft.DocAsCode.Metadata.ManagedReference.ExtractMetadataWorker+<ResolveAndExportYamlMetadata>d__19.MoveNext () [0x00170] in <5c540df9fbb940bba18cdc38a60fa384>:0
  at System.Collections.Generic.List`1[T].AddEnumerable (System.Collections.Generic.IEnumerable`1[T] enumerable) [0x00059] in <d13c8b563008422a8c5aaec0a74089cc>:0
  at System.Collections.Generic.List`1[T]..ctor (System.Collections.Generic.IEnumerable`1[T] collection) [0x00062] in <d13c8b563008422a8c5aaec0a74089cc>:0
  at System.Linq.Enumerable.ToList[TSource] (System.Collections.Generic.IEnumerable`1[T] source) [0x00018] in <c5bd4c865d5d48d3b3ebe4d522fd6bd7>:0
  at Microsoft.DocAsCode.Metadata.ManagedReference.ExtractMetadataWorker.SaveAllMembersFromCacheAsync () [0x00be1] in <5c540df9fbb940bba18cdc38a60fa384>:0
  at Microsoft.DocAsCode.Metadata.ManagedReference.ExtractMetadataWorker.ExtractMetadataAsync () [0x000c0] in <5c540df9fbb940bba18cdc38a60fa384>:0
```

## docker run with github actions

https://github.com/farcasclaudiu/docfx_conflict/runs/1567179845?check_suite_focus=true#step:6:6

```shell
Run actions/upload-artifact@v2
Uploads are case insensitive: /home/runner/work/docfx_conflict/docfx_conflict/docfx_project/_site/api/docfx_conflict.member.html was detected that it will be overwritten by another file with the same path
With the provided path, there will be 30 file(s) uploaded
Total size of all the files uploaded is 437324 bytes
Finished uploading artifact docgen. Reported size is 437324 bytes. There were 0 items that failed to upload
Artifact docgen has been successfully uploaded!
```
Artifact:

https://github.com/farcasclaudiu/docfx_conflict/actions/runs/426866005#artifacts

After extraction only one Member file is available because of being overwriten :(
