local script = {
    name = "changeCloudSize",
    displayName = "Change Cloud Size",
    parameters = {
        small = "Yes",
    },
    fieldOrder = {
        "small"
    },
    fieldInformation = {
        small = {
            fieldType = "loennScripts.dropdown",
            options = {
                "Yes", "No"
            }
        }
    },
    tooltip = "Changes the settings of all vanilla clouds",
    tooltips = {
        small = "Whether the platform is small or not",
    },
}

function script.run(room, args)
    local small = args.small

    for _, entity in ipairs(room.entities) do
        if entity._name == "cloud" then
            if small == "Yes" then
                entity.small = true
            elseif small == "No" then
                entity.small = false
            end
        end
    end
end

return script
