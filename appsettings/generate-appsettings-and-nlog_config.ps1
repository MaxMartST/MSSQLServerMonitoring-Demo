function AddPropertyRecurse($source, $toExtend){
    foreach($Property in $source | Get-Member -type NoteProperty, Property){
        if($toExtend.$($Property.Name) -eq $null){
          $toExtend | Add-Member -MemberType NoteProperty -Value $source.$($Property.Name) -Name $Property.Name `
        }
        else{
           $toExtend.$($Property.Name) = AddPropertyRecurse $source.$($Property.Name) $toExtend.$($Property.Name)
        }
    }

    return $toExtend
}
function Json-Merge($source, $extend){
	$extended = AddPropertyRecurse $extend $source
	return $extended
}
function Write-Content($path, $content){
	$Utf8NoBomEncoding = New-Object System.Text.UTF8Encoding $False
	[System.IO.File]::WriteAllLines($path, $content, $Utf8NoBomEncoding)
}
function Write-Json($path, $obj){
	$json = $obj | ConvertTo-Json -Depth 10
	Write-Content $path $json
}

$envs = @( "novoselov" )

$dir = dir . | ?{$_.PSISContainer}

foreach ($d in $dir){
	$path = Get-Content "$($d)\_path" -Raw

	$fbase = "base.appsettings.json"
	$fapp = "$($d.FullName)\appsettings.json"

	Write-Host $fapp
	$baseJson = Get-Content $fbase | out-string | ConvertFrom-Json
	$appJson = Get-Content $fapp | out-string | ConvertFrom-Json
	if($appJson){
		$appSettings = Json-Merge $baseJson $appJson
	}
	else{
		$appSettings = $baseJson
	}

	Write-Json "$($path)\appsettings.json" $appSettings; 

	foreach ($env in $envs){
		$fenv = "$($d.FullName)\appsettings.$($env).json"
		if (Test-Path $fenv) {
			Copy-Item $fenv "$($path)\appsettings.$($env).json"		
		}
		else{
			$blank = ""
			Write-Content "$($path)\appsettings.$($env).json" $blank
		}
	}

	Copy-Item "nlog.config" "$($path)\nlog.config"
}