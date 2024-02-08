local script = {
    name = "movingSpinnerChange",
    displayName = "Change Moving Spinners",
    parameters = {
        type = "Blade",
    },
    fieldOrder = {
        "type"
    },
    fieldInformation = {
        type = {
            fieldType = "loennScripts.dropdown",
            options = {
                "Blade", "Dust", "Star"
            }
        }
    },
    tooltip = "Changes the settings of all vanilla moving spinners",
    tooltips = {
        type = "What kind of type the spinner is",
    },
}

function script.run(room, args)
    local type = args.type

    for _, entity in ipairs(room.entities) do
        if entity._name == "trackSpinner" or entity._name == "rotateSpinner" then
            if type == "Dust" then
                entity.dust = true
                entity.star = false
            end

            if type == "Star" then
                entity.dust = false
                entity.star = true
            end

            if type == "Blade" then
                entity.dust = false
                entity.star = false
            end
        end
    end
end

return script
