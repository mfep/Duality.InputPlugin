<root dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.InputMappingRes" id="129723834">
  <assetInfo />
  <virtualButtonDict dataType="Struct" type="System.Collections.Generic.Dictionary`2[[System.String],[MFEP.Duality.Plugins.InputPlugin.VirtualButton]]" id="427169525" surrogate="true">
    <header />
    <body>
      <Left dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.VirtualButton" id="1100841590">
        <_x003C_Name_x003E_k__BackingField dataType="String">Left</_x003C_Name_x003E_k__BackingField>
        <associatedKeys dataType="Struct" type="System.Collections.Generic.HashSet`1[[Duality.Input.Key]]" id="2824927200">
          <m_buckets dataType="Array" type="System.Int32[]" id="3329633244">0, 0, 2</m_buckets>
          <m_comparer dataType="Struct" type="System.Collections.Generic.EnumEqualityComparer`1[[Duality.Input.Key]]" id="2319395094" />
          <m_count dataType="Int">2</m_count>
          <m_freeList dataType="Int">-1</m_freeList>
          <m_lastIndex dataType="Int">2</m_lastIndex>
          <m_siInfo />
          <m_slots dataType="Array" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]][]" id="3403136328" length="3">
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">47</hashCode>
              <next dataType="Int">-1</next>
              <value dataType="Enum" type="Duality.Input.Key" name="Left" value="47" />
            </item>
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">83</hashCode>
              <next dataType="Int">0</next>
              <value dataType="Enum" type="Duality.Input.Key" name="A" value="83" />
            </item>
          </m_slots>
          <m_version dataType="Int">2</m_version>
        </associatedKeys>
        <ButtonChanged dataType="Delegate" type="System.Action" id="3573126030" multi="true">
          <method dataType="MemberInfo" id="1941610290" value="M:MFEP.Duality.Plugins.InputPlugin.InputManager+&lt;&gt;c:&lt;RegisterButton&gt;b__6_0" />
          <target dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.InputManager+&lt;&gt;c" id="2510774090" />
          <invocationList dataType="Array" type="System.Delegate[]" id="2374695298">
            <item dataType="ObjectRef">3573126030</item>
          </invocationList>
        </ButtonChanged>
      </Left>
      <Down dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.VirtualButton" id="649525530">
        <_x003C_Name_x003E_k__BackingField dataType="String">Down</_x003C_Name_x003E_k__BackingField>
        <associatedKeys dataType="Struct" type="System.Collections.Generic.HashSet`1[[Duality.Input.Key]]" id="411997508">
          <m_buckets dataType="Array" type="System.Int32[]" id="3760513604">0, 1, 2</m_buckets>
          <m_comparer dataType="ObjectRef">2319395094</m_comparer>
          <m_count dataType="Int">2</m_count>
          <m_freeList dataType="Int">-1</m_freeList>
          <m_lastIndex dataType="Int">2</m_lastIndex>
          <m_siInfo />
          <m_slots dataType="Array" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]][]" id="3884632726" length="3">
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">46</hashCode>
              <next dataType="Int">-1</next>
              <value dataType="Enum" type="Duality.Input.Key" name="Down" value="46" />
            </item>
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">101</hashCode>
              <next dataType="Int">-1</next>
              <value dataType="Enum" type="Duality.Input.Key" name="S" value="101" />
            </item>
          </m_slots>
          <m_version dataType="Int">2</m_version>
        </associatedKeys>
        <ButtonChanged dataType="ObjectRef">3573126030</ButtonChanged>
      </Down>
      <Up dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.VirtualButton" id="3234930070">
        <_x003C_Name_x003E_k__BackingField dataType="String">Up</_x003C_Name_x003E_k__BackingField>
        <associatedKeys dataType="Struct" type="System.Collections.Generic.HashSet`1[[Duality.Input.Key]]" id="628691392">
          <m_buckets dataType="Array" type="System.Int32[]" id="1062550300">2, 0, 0</m_buckets>
          <m_comparer dataType="ObjectRef">2319395094</m_comparer>
          <m_count dataType="Int">2</m_count>
          <m_freeList dataType="Int">-1</m_freeList>
          <m_lastIndex dataType="Int">2</m_lastIndex>
          <m_siInfo />
          <m_slots dataType="Array" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]][]" id="381632022" length="3">
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">45</hashCode>
              <next dataType="Int">-1</next>
              <value dataType="Enum" type="Duality.Input.Key" name="Up" value="45" />
            </item>
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">105</hashCode>
              <next dataType="Int">0</next>
              <value dataType="Enum" type="Duality.Input.Key" name="W" value="105" />
            </item>
          </m_slots>
          <m_version dataType="Int">2</m_version>
        </associatedKeys>
        <ButtonChanged dataType="ObjectRef">3573126030</ButtonChanged>
      </Up>
      <Right dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.VirtualButton" id="1841287930">
        <_x003C_Name_x003E_k__BackingField dataType="String">Right</_x003C_Name_x003E_k__BackingField>
        <associatedKeys dataType="Struct" type="System.Collections.Generic.HashSet`1[[Duality.Input.Key]]" id="833611236">
          <m_buckets dataType="Array" type="System.Int32[]" id="650027460">1, 0, 2</m_buckets>
          <m_comparer dataType="Struct" type="System.Collections.Generic.EnumEqualityComparer`1[[Duality.Input.Key]]" id="1447494038" />
          <m_count dataType="Int">2</m_count>
          <m_freeList dataType="Int">-1</m_freeList>
          <m_lastIndex dataType="Int">2</m_lastIndex>
          <m_siInfo />
          <m_slots dataType="Array" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]][]" id="3919863936" length="3">
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">48</hashCode>
              <next dataType="Int">-1</next>
              <value dataType="Enum" type="Duality.Input.Key" name="Right" value="48" />
            </item>
            <item dataType="Struct" type="System.Collections.Generic.HashSet`1+Slot[[Duality.Input.Key]]">
              <hashCode dataType="Int">86</hashCode>
              <next dataType="Int">-1</next>
              <value dataType="Enum" type="Duality.Input.Key" name="D" value="86" />
            </item>
          </m_slots>
          <m_version dataType="Int">2</m_version>
        </associatedKeys>
        <ButtonChanged dataType="Delegate" type="System.Action" id="946826262" multi="true">
          <method dataType="ObjectRef">1941610290</method>
          <target dataType="Struct" type="MFEP.Duality.Plugins.InputPlugin.InputManager+&lt;&gt;c" id="1912074158" />
          <invocationList dataType="Array" type="System.Delegate[]" id="920794826">
            <item dataType="ObjectRef">946826262</item>
          </invocationList>
        </ButtonChanged>
      </Right>
    </body>
  </virtualButtonDict>
</root>
<!-- XmlFormatterBase Document Separator -->
