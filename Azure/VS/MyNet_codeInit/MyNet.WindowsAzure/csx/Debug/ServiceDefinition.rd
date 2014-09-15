<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="MyNet.WindowsAzure" generation="1" functional="0" release="0" Id="ce719940-d8ff-4ad3-bb4f-06cabcdccf87" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="MyNet.WindowsAzureGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="MyNet.Webservices:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/LB:MyNet.Webservices:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="MyNet.Webservices:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MapMyNet.Webservices:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="MyNet.WebservicesInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MapMyNet.WebservicesInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:MyNet.Webservices:Endpoint1">
          <toPorts>
            <inPortMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.Webservices/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapMyNet.Webservices:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.Webservices/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapMyNet.WebservicesInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.WebservicesInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="MyNet.Webservices" generation="1" functional="0" release="0" software="C:\Users\Valiani\Desktop\Stage\MyNet_codeInit\MyNet.WindowsAzure\csx\Debug\roles\MyNet.Webservices" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="8080" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;MyNet.Webservices&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;MyNet.Webservices&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.WebservicesInstances" />
            <sCSPolicyUpdateDomainMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.WebservicesUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.WebservicesFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="MyNet.WebservicesUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="MyNet.WebservicesFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="MyNet.WebservicesInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="9cdf2a56-a025-4613-9fa7-4f8328de88ed" ref="Microsoft.RedDog.Contract\ServiceContract\MyNet.WindowsAzureContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="aeff10eb-8fe2-4cca-b5df-0915eae61344" ref="Microsoft.RedDog.Contract\Interface\MyNet.Webservices:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/MyNet.WindowsAzure/MyNet.WindowsAzureGroup/MyNet.Webservices:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>