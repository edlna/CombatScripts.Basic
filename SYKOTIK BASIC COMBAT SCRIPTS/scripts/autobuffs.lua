local IM = CppEnums.EProcessorInputMode
local VCK = CppEnums.VirtualKeyCode

local GlobalKeyBinder_CheckKeyPressed = function(keyCode)
	return isKeyDown_Once(keyCode)
end

function InitAutoPot()
	local ScreenX = getScreenSizeX()
	local ScreenY = getScreenSizeY()
	local wSizeX = 850   -- panel size
	local wSizeY = 550  -- panel size
	local posx = (ScreenX / 2) - (wSizeX / 2) -- sets panel pos to center screen
	local posy = (ScreenY /2) - (wSizeY / 2)  -- sets panel pos to center screen
	
	local tempField = UI.getChildControl( Panel_DetectPlayer, "Edit_PlayerName"	)
	local tempBtn = UI.getChildControl(Panel_CustomizationFrame, "CheckButton_UseFaceCustomizationHair")
	
	pPanel = UI.createPanel ("SetupPanel", 6001)
	pPanel:SetShow(false, false)
	pPanel:SetDragEnable(true)
	pPanel:SetIgnore(false)
	pPanel:SetSize(wSizeX,  wSizeY )
	pPanel:SetPosX(posx)
	pPanel:SetPosY(posy)
	pPanel:ChangeTextureInfoName( "New_UI_Common_forLua/Default/window_teal_deco.dds" )
	pPanel:SetAlpha(1)
	pPanel:SetDragAll( true )
	
	local pFrame = UI.createControl(CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "Frame_PPanel")		
	pFrame:SetSize(830, 50)	
	pFrame:SetPosX(10)  
	pFrame:SetPosY(30)   	
	pFrame:SetText("<PAColor0x00FFFFFF>Auto Buff Options<PAOldColor>")

-- French?
	
	local FrnchText = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "FrenchTxt" )
    FrnchText:SetSize( 50 , 40 )	
	FrnchText:SetPosX( 650 )
	FrnchText:SetPosY( 70 )	
	FrnchText:SetText("Français?")
	FrnchText:SetTextHorizonLeft()
	
	Btn_FrEn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_French")
    CopyBaseProperty( tempBtn, Btn_FrEn )
	Btn_FrEn:SetPosX( 720 )
	Btn_FrEn:SetPosY( 80 )
	Btn_FrEn:SetCheck(false)
	
-- HP Section	
	
	local HPEnText = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "HPEnableTxt" )
    HPEnText:SetSize( 50 , 40 )	
	HPEnText:SetPosX( 30 )
	HPEnText:SetPosY( 70 )	
	HPEnText:SetText("Enabled: ")
	HPEnText:SetTextHorizonLeft()
	
	Btn_HPEn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_HPEn")
    CopyBaseProperty( tempBtn, Btn_HPEn )
	Btn_HPEn:SetPosX( 100 )
	Btn_HPEn:SetPosY( 80 )
	Btn_HPEn:SetCheck(true)
	
	local HPThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "HP_Threshold" )
    HPThres:SetSize( 50 , 40 )	
	HPThres:SetPosX( 150 )
	HPThres:SetPosY( 70 )	
	HPThres:SetText("Set HP Threshold")
	HPThres:SetTextHorizonLeft()
	
	Edt_HPThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Edit_HPThreshold")
	CopyBaseProperty( tempField, Edt_HPThres )
	Edt_HPThres:SetPosX( 300 )
	Edt_HPThres:SetPosY( 70 )
	Edt_HPThres:SetSize(60, 40)
	Edt_HPThres:SetEditText(HPThreshold, true)
	
-- MP Section
	
	local MPEnText = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "MPEnableTxt" )
    MPEnText:SetSize( 50 , 40 )	
	MPEnText:SetPosX( 30 )
	MPEnText:SetPosY( 140 )	
	MPEnText:SetText("Enabled: ")
	MPEnText:SetTextHorizonLeft()
	
	Btn_MPEn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_MPEn")
    CopyBaseProperty( tempBtn, Btn_MPEn )
	Btn_MPEn:SetPosX( 100 )
	Btn_MPEn:SetPosY( 150 )
	Btn_MPEn:SetCheck(true)
	
	local MPThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "MP_Threshold" )
    MPThres:SetSize( 10 , 10 )	
	MPThres:SetPosX( 150 )
	MPThres:SetPosY( 150 )	
	MPThres:SetText("Set MP Threshold")
	MPThres:SetTextHorizonLeft()
	
	Edt_MPThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Edit_MPThreshold")
    CopyBaseProperty( tempField, Edt_MPThres )
	Edt_MPThres:SetPosX( 300 )
	Edt_MPThres:SetPosY( 140 )
	Edt_MPThres:SetSize(60, 40)
	Edt_MPThres:SetEditText(MPThreshold, true)

-- Pet Section	
	
	local PetEnText = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "PetEnableTxt" )
    PetEnText:SetSize( 50 , 40 )	
	PetEnText:SetPosX( 30 )
	PetEnText:SetPosY( 210 )	
	PetEnText:SetText("Enabled: ")
	PetEnText:SetTextHorizonLeft()
	
	Btn_PetEn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_PetEn")
    CopyBaseProperty( tempBtn, Btn_PetEn )
	Btn_PetEn:SetPosX( 100 )
	Btn_PetEn:SetPosY( 220 )
	Btn_PetEn:SetCheck(true)

	local PetThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "Pet_Threshold" )
    PetThres:SetSize( 10 , 10 )	
	PetThres:SetPosX( 150 )
	PetThres:SetPosY( 220 )	
	PetThres:SetText("Set Pet Threshold")
	PetThres:SetTextHorizonLeft()
	
	Edt_PetThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Edit_PetThreshold")
    CopyBaseProperty( tempField, Edt_PetThres )
	Edt_PetThres:SetPosX( 300 )
	Edt_PetThres:SetPosY( 210 )
	Edt_PetThres:SetSize(60, 40)
	Edt_PetThres:SetEditText(petThreshold, true)
	
-- Food Section	
	
	local FoodEnText = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "FoodEnableTxt" )
    FoodEnText:SetSize( 50 , 40 )	
	FoodEnText:SetPosX( 30 )
	FoodEnText:SetPosY( 280 )	
	FoodEnText:SetText("Enabled: ")
	FoodEnText:SetTextHorizonLeft()
	
	Btn_FoodEn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_FoodEn")
    CopyBaseProperty( tempBtn, Btn_FoodEn )
	Btn_FoodEn:SetPosX( 100 )
	Btn_FoodEn:SetPosY( 290 )
	Btn_FoodEn:SetCheck(false)

	local FoodThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "Food_Threshold" )
    FoodThres:SetSize( 10 , 10 )	
	FoodThres:SetPosX( 150 )
	FoodThres:SetPosY( 290 )	
	FoodThres:SetText("Enter Food Name")
	FoodThres:SetTextHorizonLeft()
	
	Edt_Food = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Food Name")
    CopyBaseProperty( tempField, Edt_Food )
	Edt_Food:SetPosX( 300 )
	Edt_Food:SetPosY( 280 )
	
-- Food Section	2
	
	local FoodEnText2 = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "FoodEnableTxt2" )
    FoodEnText2:SetSize( 50 , 40 )	
	FoodEnText2:SetPosX( 30 )
	FoodEnText2:SetPosY( 350 )	
	FoodEnText2:SetText("Enabled: ")
	FoodEnText2:SetTextHorizonLeft()
	
	Btn_FoodEn2 = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_FoodEn2")
    CopyBaseProperty( tempBtn, Btn_FoodEn2 )
	Btn_FoodEn2:SetPosX( 100 )
	Btn_FoodEn2:SetPosY( 360 )
	Btn_FoodEn2:SetCheck(false)

	local FoodThres2 = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "Food_Threshold2" )
    FoodThres2:SetSize( 10 , 10 )	
	FoodThres2:SetPosX( 150 )
	FoodThres2:SetPosY( 360 )	
	FoodThres2:SetText("Enter Food Name")
	FoodThres2:SetTextHorizonLeft()
	
	Edt_Food2 = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Food Name2")
    CopyBaseProperty( tempField, Edt_Food2 )
	Edt_Food2:SetPosX( 300 )
	Edt_Food2:SetPosY( 350 )
	
-- Node Investment section
	
	local NodeEnText = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "NodeEnableTxt" )
    NodeEnText:SetSize( 50 , 40 )	
	NodeEnText:SetPosX( 30 )
	NodeEnText:SetPosY( 420 )	
	NodeEnText:SetText("Enabled: ")
	NodeEnText:SetTextHorizonLeft()
	
	Btn_NodeEn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_CHECKBUTTON, pPanel, "Button_NodeEnable")
    CopyBaseProperty( tempBtn, Btn_NodeEn )
	Btn_NodeEn:SetPosX( 100 )
	Btn_NodeEn:SetPosY( 430 )
	Btn_NodeEn:SetCheck(false)

	local NodeNameTxt = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "Node_Name_Txt" )
    NodeNameTxt:SetSize( 10 , 10 )	
	NodeNameTxt:SetPosX( 150 )
	NodeNameTxt:SetPosY( 430 )	
	NodeNameTxt:SetText("Enter node name")
	NodeNameTxt:SetTextHorizonLeft()
	
	Edt_NodeName = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Node_Name_Field")
    CopyBaseProperty( tempField, Edt_NodeName )
	Edt_NodeName:SetPosX( 300 )
	Edt_NodeName:SetPosY( 420 )
	
	local EnergyThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_STATICTEXT, pPanel, "Energy_Threshold" )
    EnergyThres:SetSize( 10 , 10 )	
	EnergyThres:SetPosX( 550 )
	EnergyThres:SetPosY( 430 )	
	EnergyThres:SetText("Set Energy Threshold")
	EnergyThres:SetTextHorizonLeft()
	
	Edt_EnergyThres = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_EDIT, pPanel, "Energy_Threshold_Field")
    CopyBaseProperty( tempField, Edt_EnergyThres )
	Edt_EnergyThres:SetPosX( 700 )
	Edt_EnergyThres:SetPosY( 420 )
	Edt_EnergyThres:SetSize(60, 40)
	Edt_EnergyThres:SetEditText(energyThreshold, true)
	
-- Save and Close

   local closeBtn = UI.createControl( CppEnums.PA_UI_CONTROL_TYPE.PA_UI_CONTROL_BUTTON, pPanel, "Btn_Cls")
   closeBtn:addInputEvent("Mouse_LUp", "HandleClickedClose()")
   closeBtn:SetSize( 115,  20 )	
   closeBtn:ActiveMouseEventEffect( true )
   closeBtn:SetPosX( (pPanel:GetSizeX())/2 - 60 )
   closeBtn:SetPosY( (pPanel:GetSizeY()) - 80 )		
   closeBtn:ChangeTextureInfoName("New_UI_Common_forLua/Default/Default_Buttons.dds")
   local x1, y1, x2, y2 = setTextureUV_Func( closeBtn, 0, 130, 85, 152 )
   closeBtn:getBaseTexture():setUV(  x1, y1, x2, y2  )
   closeBtn:setRenderTexture(closeBtn:getBaseTexture())		
   closeBtn:SetText("Save and Close")
end

function HandleClickedClose()
	HPThreshold = tonumber(Edt_HPThres:GetEditText())
	MPThreshold = tonumber(Edt_MPThres:GetEditText())
	petThreshold = tonumber(Edt_PetThres:GetEditText())
	energyThreshold = tonumber(Edt_EnergyThres:GetEditText())
	
	foodName = Edt_Food:GetEditText()
	foodName2 = Edt_Food2:GetEditText()
	nodeName = Edt_NodeName:GetEditText()
	
	AutoHPEn = Btn_HPEn:IsCheck()
	AutoMPEn = Btn_MPEn:IsCheck()
	AutoPetEn = Btn_PetEn:IsCheck()
	AutoFoodEn = Btn_FoodEn:IsCheck()
	AutoFoodEn2 = Btn_FoodEn2:IsCheck()
	AutoNodeEn = Btn_NodeEn:IsCheck()
	FrenchEn = Btn_FrEn:IsCheck()
	
	local wSettings = {
		Settings = {
			hp = HPThreshold,
			mp = MPThreshold,
			pet = petThreshold,
			energy=energyThreshold,
			food1 = foodName,
			food2 = foodName2,
			node = nodeName		
		},
		CheckOptions = {
			opthp = tostring(AutoHPEn),
			optmp = tostring(AutoMPEn),
			optpet = tostring(AutoPetEn),
			optfood1 = tostring(AutoFoodEn),
			optfood2 = tostring(AutoFoodEn2),
			optnode = tostring(AutoNodeEn),
			optFrench = tostring(FrenchEn)
		}
	}
	
	SaveSettings(SettingsFile, wSettings)
	
	R_OnEnergyChanged()
	R_FoodCheck()
	
	menuEnabled = false
    UI.Set_ProcessorInputMode(IM.eProcessorInputMode_UiMode)		  
    pPanel:SetShow(false, false)
	ClearFocusEdit()
end

function Display_Menu()
	if isKeyPressed(VCK.KeyCode_MENU) and GlobalKeyBinder_CheckKeyPressed(VCK.KeyCode_P)  then
		if menuEnabled == true then
			menuEnabled = false
			UI.Set_ProcessorInputMode(IM.eProcessorInputMode_UiMode)		  
			pPanel:SetShow(false, false)
			ClearFocusEdit()
		else 
			menuEnabled = true 
			UI.Set_ProcessorInputMode(IM.eProcessorInputMode_ChattingInputMode)
			pPanel:SetShow(true, false)	   	  
		end
   end
end

--Function that finds the items to be used
function R_GetItemSlot(name) 
   local inventory = getSelfPlayer():get():getInventory()
   local invenSize = inventory:size()
   local loop = invenSize - 1
   for ii=1 , loop, 1 do
      local itemWrapper = getInventoryItem( ii )
      if( nil ~= itemWrapper ) then
		local itemString = tostring(itemWrapper:getStaticStatus():getName())
         if string.find(itemString, name) ~= nil then
            return ii
		 end
      end
   end 
   return false
end

-- Function that checks your HP and uses potions
function R_HealthCheck()
	if AutoHPEn == false then
		return
	end
	local player = getSelfPlayer():get()
	local hp = player:getHp()
	local maxHp = player:getMaxHp()
	local percentHp = ( hp / maxHp * 100 )
	local hpPotion = false

	if HPThreshold > percentHp and hp > 0 then
		if FrenchEn then
			hpPotion = R_GetItemSlot("Jus de grain")
			if hpPotion ~= false then
				inventoryUseItem (CppEnums.ItemWhereType.eInventory, hpPotion, nil, true )
			else
				hpPotion = R_GetItemSlot("Potion de santé")
				if hpPotion ~= false then
					inventoryUseItem (CppEnums.ItemWhereType.eInventory, hpPotion, nil, true )
				end
			end
		else
			hpPotion = R_GetItemSlot("Grain Juice")
			if hpPotion ~= false then
				inventoryUseItem (CppEnums.ItemWhereType.eInventory, hpPotion, nil, true )
			else
				hpPotion = R_GetItemSlot("HP Potion")
				if hpPotion ~= false then
					inventoryUseItem (CppEnums.ItemWhereType.eInventory, hpPotion, nil, true )
				end
			end
		end
	end
end

-- Function that checks your MP and uses potions
function R_ManaCheck()
	if AutoMPEn == false then
		return
	end
	local player = getSelfPlayer():get()
	local mp = player:getMp()
	local maxMp = player:getMaxMp()
	local percentMp = ( mp / maxMp * 100 )
	local mpPotion = false

	if MPThreshold > percentMp then
		if FrenchEn then
			mpPotion = R_GetItemSlot("Infusion")
			if mpPotion ~= false then
				inventoryUseItem (CppEnums.ItemWhereType.eInventory, mpPotion, nil, true )
			else
				mpPotion = R_GetItemSlot("Potion de mana")
				if mpPotion ~= false then
					inventoryUseItem (CppEnums.ItemWhereType.eInventory, mpPotion, nil, true )
				end
			end
		else
			mpPotion = R_GetItemSlot("Herbal Juice")
			if mpPotion ~= false then
				inventoryUseItem (CppEnums.ItemWhereType.eInventory, mpPotion, nil, true )
			else
				mpPotion = R_GetItemSlot("MP Potion")
				if mpPotion ~= false then
					inventoryUseItem (CppEnums.ItemWhereType.eInventory, mpPotion, nil, true )
				end
			end
		end
	end
end

-- Function that checks your pets and feeds them as needed
function R_PetCheck()
	if AutoPetEn == false then
		return
	end

	local petCount = ToClient_getPetUnsealedList()
	if petCount == 0 then
		return
	end
	
	local petFeed = false

	for index = 0, petCount - 1 do
		local pcPetData = ToClient_getPetUnsealedDataByIndex( index )
		if nil ~= pcPetData then
			local petStaticStatus   = pcPetData:getPetStaticStatus()
			local petHungry         = pcPetData:getHungry()
			local petMaxHungry      = petStaticStatus._maxHungry
			local petHungryPercent   = ( petHungry / petMaxHungry ) * 100

			if petThreshold > petHungryPercent then
				if FrenchEn then
					petFeed = R_GetItemSlot("Nourriture")
				else
					petFeed = R_GetItemSlot("Feed")
				end
				if petFeed ~= false then
					inventoryUseItem (CppEnums.ItemWhereType.eInventory, petFeed, nil, true )
				end
			end
		end
	end
end

-- Function to eat food as needed
function R_FoodCheck()
   if ((AutoFoodEn == false) and (AutoFoodEn2 == false)) then
		return
   end
   if AutoFoodEn then
		local foodSlot = R_GetItemSlot(foodName)
		if foodSlot ~= false then
			inventoryUseItem(CppEnums.ItemWhereType.eInventory, foodSlot, nil, true )
		end
	end
	
	if AutoFoodEn2 then
		local foodSlot2 = R_GetItemSlot(foodName2)
		if foodSlot2 ~= false then
			inventoryUseItem(CppEnums.ItemWhereType.eInventory, foodSlot2, nil, true )
		end
	end
end

function Crystal_InvestInNode(name) 
   local regionInfoCount = getRegionInfoCount()

   for index  = 0, regionInfoCount -1 do
      local regionInfo = getRegionInfo(index)
      if regionInfo:getAreaName() == name then
         ToClient_RequestIncreaseExperienceNode(regionInfo:getExplorationKey(), 10)
         return regionInfo:getExplorationKey()
      end
   end
   return 0
end

function R_OnEnergyChanged()
   if (getSelfPlayer():getWp() / getSelfPlayer():getMaxWp() * 100) > energyThreshold then
      Crystal_InvestInNode(nodeName)
   end
end

function FreeMem()
    collectgarbage("collect")
end

function GetSettings(file)
   local file = io.open(file, "r")
	  if file==nil then
     return nil
      end
    local data = {}
    local rejected = {}
    local parent = data
    local i = 0
    local m, n
    local function parse(line)
        local m, n
        m,n = line:match("^([%w%p]-)=(.*)$")
        if m then
            parent[m] = n
            return true
        end
        m = line:match("^%[([%w%p]+)%][%s]*")
        if m then
            data[m] = {}
            parent = data[m]		
            return true
        end
        if line:match("^$") then
            return true
        end
        if line:match("^#") then
            return true
        end
        return false
    end
    for line in file:lines() do
        i = i + 1	
        if not parse(line) then
            table.insert(rejected, i)
        end
    end
    file:close()
    return data
end

function txtBoolToBool(boolval)
    if boolval == "true" then
        return true
    else
        return false
    end
end

function SaveSettings(file, data)
    if type(data) ~= "table" then return nil end
    local file = io.open(file, "w")
    if not file then
        return nil
    end
    for s,t in pairs(data) do
        file:write(string.format("[%s]\n", s))
        for k,v in pairs(t) do
            file:write(string.format("%s=%s\n", tostring(k), tostring(v)))
        end
    end
    file:close()
    return true
end

function FileExists(file)
  local file=io.open(file, "r")      
    if file==nil then
       FileExists = false 
    else
       file:close()
       file_found = true
    end
    return FileExists
end

if R_isEnabled ~= nil then
   -- UI.deletePanel("SetupPanel")
   -- pPanel = nil
   -- R_isEnabled = nil
   -- unregisterEvent("FromClient_SelfPlayerHpChanged", "R_HealthCheck")
   -- unregisterEvent("FromClient_SelfPlayerMpChanged", "R_ManaCheck")
   -- unregisterEvent("FromClient_PetInfoChanged",   "R_PetCheck")
   -- unregisterEvent("ResponseBuff_changeBuffList",	"R_FoodCheck")
   -- unregisterEvent("EventGlobalKeyBinder", "Display_Menu")
   -- unregisterEvent("FromClient_WpChanged", "Crystal_OnEnergyChanged")
   -- FreeMem()
else
	pPanel = nil
	menuEnabled = false

	-- Change these default values as desired
	local DefaultSettings = {
		Settings = {
			hp = 50,
			mp = 50,
			pet = 50,
			energy=50,
			food1 = "",
			food2 = "",
			node = ""		
		},
		CheckOptions = {
			opthp = true,
			optmp = true,
			optpet = true,
			optfood1 = false,
			optfood2 = false,
			optnode = false,
			optFrench = false
		}
	}
	
	SettingsFile = "..\\scripts\\settings.lua" 
	if FileExists(SettingsFile) == false then
		SaveSettings(SettingsFile,  DefaultSettings  )
	end
   
	local rSettings = GetSettings(SettingsFile)
	
	foodName = rSettings["Settings"]["food1"] 
	foodName2 = rSettings["Settings"]["food2"] 
	HPThreshold = tonumber(rSettings["Settings"]["hp"])
	MPThreshold = tonumber(rSettings["Settings"]["mp"] )
	petThreshold = tonumber(rSettings["Settings"]["pet"] )
	energyThreshold = tonumber(rSettings["Settings"]["energy"] )
	nodeName = rSettings["Settings"]["node"] 

	InitAutoPot()
	
	Edt_Food:SetEditText(foodName)
	Edt_Food2:SetEditText(foodName2)
	Edt_NodeName:SetEditText(nodeName)
	Btn_HPEn:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["opthp"]))
	Btn_MPEn:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["optmp"]))
	Btn_PetEn:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["optpet"]))
	Btn_FoodEn:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["optfood1"]))
	Btn_FoodEn2:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["optfood2"]))
	Btn_NodeEn:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["optnode"]))
	Btn_FrEn:SetCheck(txtBoolToBool(rSettings["CheckOptions"]["optFrench"]))
	AutoHPEn = Btn_HPEn:IsCheck()
	AutoMPEn = Btn_MPEn:IsCheck()
	AutoPetEn = Btn_PetEn:IsCheck()
	AutoFoodEn = Btn_FoodEn:IsCheck()
	AutoFoodEn2 = Btn_FoodEn2:IsCheck()
	AutoNodeEn = Btn_NodeEn:IsCheck()
	FrenchEn = Btn_FrEn:IsCheck()
	
	Proc_ShowBigMessage_Ack_WithOut_ChattingMessage({main = "Auto Buffs", sub = "Script is loaded"})

	registerEvent("FromClient_SelfPlayerHpChanged", "R_HealthCheck")
	registerEvent("FromClient_SelfPlayerMpChanged", "R_ManaCheck")
	registerEvent("FromClient_PetInfoChanged",   "R_PetCheck")
	registerEvent("ResponseBuff_changeBuffList",	"R_FoodCheck")
	registerEvent("EventGlobalKeyBinder", "Display_Menu")
	registerEvent("FromClient_WpChanged", "R_OnEnergyChanged")
	R_isEnabled = true
end