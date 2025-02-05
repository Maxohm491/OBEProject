Sub packages are included for URP and HDRP, SRP is extracted by default.

SRP = Standard Render Pipeline, 
URP = Universal Render Pipeline, 
HDRP = High Definition Render Pipeline

Materials will be extracted to their corresponding render pipeline folder (SRP, HDRP, URP) in the Materials folder inside
GreenMossGames (GreenMossGames/UltimateMaterialLibrary/Materials/[render pipeline])

**Advice**
I would recommend making a copy of the materials you want to use into a separate material folder so you
can manually adust tiling and other properties (color, normalmap scale, etc). This all depends on the size of the object being applied to
and the look you are going for. Check out the demo scene for examples.