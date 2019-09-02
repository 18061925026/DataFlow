for /r "%cd%" %%i in (*.pdb,*.vshost.*,*.sdf,*.suo) do (del /q/s "%%i")
for /r "%cd%" %%i in (obj,bin) do (IF EXIST "%%i" rd /q/s "%%i")
rd IVS.ImageProcess\x64  /q/s 
rd IVS.ImageProcess\Debug  /q/s 
rd IVS.ImageProcess\Release  /q/s 
rd IVS.ModuleInterface\x64  /q/s 
rd IVS.ModuleInterface\Debug  /q/s 
rd IVS.ModuleInterface\Release  /q/s 
del IVS.ModuleInterface\*.obj 
rd IVS.GPUImageEnhancement\Debug  /q/s 
rd IVS.GPUImageEnhancement\Release  /q/s 
rd IVS.GPUImageEnhancement\x64  /q/s 
rd Debug /q/s 
rd Release /q/s 
rd Iart.Src\controller\iARTView\iARTView\Debug  /q/s 
rd Iart.Src\controller\iARTView\iARTView\Release  /q/s 
del Iart.Src\controller\Include\ENVIART.H._

rd IartV2.Src\controller\iARTView\iARTView\Debug  /q/s 
rd IartV2.Src\controller\iARTView\iARTView\Release  /q/s 
del IartV2.Src\controller\Include\ENVIART.H._

rd IartUp.Src\controller\iARTView\iARTView\Debug  /q/s 
rd IartUp.Src\controller\iARTView\iARTView\Release  /q/s 
del IartUp.Src\controller\Include\ENVIART.H._

rd Iart.Src\controller\iARTView\iARTView\Debug  /q/s 
rd Iart.Src\controller\iARTView\iARTView\Release  /q/s 
del Iart.Src\controller\Include\ENVIART.H._

rd IartV2.Src\controller\iARTView\iARTView\Debug  /q/s 
rd IartV2.Src\controller\iARTView\iARTView\Release  /q/s 
del IartV2.Src\controller\Include\ENVIART.H._

rd IartUp.Src\controller\iARTView\iARTView\Debug  /q/s 
rd IartUp.Src\controller\iARTView\iARTView\Release  /q/s 
del IartUp.Src\controller\Include\ENVIART.H._

rd Iart.Src\controller\iARTView\iARTView\x64\Debug  /q/s 
rd Iart.Src\controller\iARTView\iARTView\x64\Release  /q/s 

rd IartV2.Src\controller\iARTView\iARTView\x64\Debug  /q/s 
rd IartV2.Src\controller\iARTView\iARTView\x64\Release  /q/s 

rd IartUp.Src\controller\iARTView\iARTView\x64\Debug  /q/s 
rd IartUp.Src\controller\iARTView\iARTView\x64\Release  /q/s 

rd Iart.Src\controller\iARTView\iARTView\x86\Debug  /q/s 
rd Iart.Src\controller\iARTView\iARTView\x86\Release  /q/s 

rd IartV2.Src\controller\iARTView\iARTView\x86\Debug  /q/s 
rd IartV2.Src\controller\iARTView\iARTView\x86\Release  /q/s 

rd IartUp.Src\controller\iARTView\iARTView\x86\Debug  /q/s 
rd IartUp.Src\controller\iARTView\iARTView\x86\Release  /q/s 

rd Iart.Src\simulator\SimLinac\ControllerInterface\x64\Debug  /q/s 
rd Iart.Src\simulator\SimLinac\ControllerInterface\x64\Release  /q/s 

rd IartV2.Src\simulator\SimLinac\ControllerInterface\x64\Debug  /q/s 
rd IartV2.Src\simulator\SimLinac\ControllerInterface\x64\Release  /q/s 

rd IartUp.Src\simulator\SimLinac\ControllerInterface\x64\Debug  /q/s 
rd IartUp.Src\simulator\SimLinac\ControllerInterface\x64\Release  /q/s 

rd Iart.Src\simulator\SimLinac\ControllerInterface\x86\Debug  /q/s 
rd Iart.Src\simulator\SimLinac\ControllerInterface\x86\Release  /q/s 

rd IartV2.Src\simulator\SimLinac\ControllerInterface\x86\Debug  /q/s 
rd IartV2.Src\simulator\SimLinac\ControllerInterface\x86\Release  /q/s 

rd IartUp.Src\simulator\SimLinac\ControllerInterface\x86\Debug  /q/s 
rd IartUp.Src\simulator\SimLinac\ControllerInterface\x86\Release  /q/s 




rd IVS.GPUImageEnhancement\x64  /q/s 
rd CBCT.FusionRegistration\x64  /q/s 
