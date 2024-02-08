local script = {
    name = "changeSpikes",
    displayName = "Change Spikes",
    parameters = {
        type = "default",
    },
    fieldOrder = {
        "type"
    },
    fieldInformation = {
        type = {
            fieldType = "loennScripts.dropdown",
            options = {
                "default", "outline", "cliffside", "reflection", "tentacles"
            }
        }
    },
    tooltip = "Changes the settings of all vanilla spikes",
    tooltips = {
        type = "The type of spikes",
    },
}

function script.run(room, args)
    local type = args.type

    for _, entity in ipairs(room.entities) do
        if entity._name == "spikesUp" or entity._name == "spikesDown" or entity._name == "spikesLeft" or entity._name == "spikesRight" then
            entity.type = type
        end
    end
end

return script
