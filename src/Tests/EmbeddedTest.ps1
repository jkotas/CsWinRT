# file EmbeddedTest.ps1 
# tests net5+ support in PowerShell v7.2+

# requires installing Pester # Install-Module Pester -Force -SkipPublisherCheck -RequiredVersion 4.4.4
Describe 'AppX test' {
	It 'Get-AppXPackage should return results' {
		$pkgs = Get-AppxPackage
		$pkgs.Count | Should -BeGreaterThan 0
		$pkgs[0].Name | Should -Not -BeNullOrEmpty
	}
}