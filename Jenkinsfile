node {
	stage 'Checkout'
		checkout scm

	stage 'Build'
		bat 'nuget restore ZetaCalc.sln'
		bat "\"${tool 'MSBuild'}\" ZetaCalc.sln /p:Configuration=Release /p:Platform=\"Any CPU\" /p:ProductVersion=1.0.0.${env.BUILD_NUMBER}"

	stage 'Archive'
		archive 'ZetaCalc/bin/Release/**'

}
