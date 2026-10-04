# NetCraft mod template

Sample code for NetCraft mod development. Every entry in `NetCraftTemplate.yaml`
points at one file under `examples/`, and `ncm template` pulls them on demand.

## Usage

```
ncm template view              list every entry
ncm template view Wrapper.?    filter by id, ? and * are wildcards
ncm template example <api id>  pull one example file into the current directory
```

## The two routes

`NetCraft.ModApi` exposes two namespaces, pick the one that fits:

| Namespace | What you get |
| --- | --- |
| `NetCraft.ModApi.Wrapper` | Events, `Nc*` facades and `Nc*` handles. No kernel type shows up in the public surface, so a kernel rename does not force a rebuild of your mod. |
| `NetCraft.ModApi.Extension` | `[Inject]` and `[Mixin]` attributes. Rules name kernel types and methods directly, which is more powerful and more fragile. |

## Adding an entry

```
python add_api.py NetCraft.ModApi.Wrapper.NcLevel --title "Level handle" --summary "Per dimension access."
```

The script appends the entry to the catalog and creates `examples/NcLevel.cs`
from a skeleton when the file does not exist yet. Comments in every example
file stay in English so the repository reads the same for everyone.
