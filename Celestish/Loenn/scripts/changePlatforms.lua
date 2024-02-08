local script = {
    name = "movingPlatformChange",
    displayName = "Change Moving Platforms",
    parameters = {
        type = "Default",
    },
    fieldOrder = {
        "type"
    },
    fieldInformation = {
        type = {
            fieldType = "loennScripts.dropdown",
            options = {
                "default", "cliffside"
            }
        }
    },
    tooltip = "Changes the settings of all vanilla moving platforms",
    tooltips = {
        type = "What kind of platform it is",
    },
}

function script.run(room, args)
    local type = args.type

    for _, entity in ipairs(room.entities) do
        if entity._name == "movingPlatform" then
            entity.texture = type
        end
    end
end

return script
