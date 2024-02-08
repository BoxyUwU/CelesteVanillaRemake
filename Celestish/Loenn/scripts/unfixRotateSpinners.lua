local script = {
    name = "unfixRotateSpinners",
    displayName = "Unfix Rotate Spinners",
    tooltip = "Changes all vanilla rotate spinners to a custom variant that does not have `fixAngle` set",
}

function script.run(room, args)
    for _, entity in ipairs(room.entities) do
        if entity._name == "rotateSpinner" then
            if entity.dust then
                entity._name = "Celestish/UnfixedDustRotateSpinner"
            elseif entity.star then
                entity._name = "Celestish/UnfixedStarRotateSpinner"
            else
                entity._name = "Celestish/UnfixedBladeRotateSpinner"
            end

            entity.dust = nil
            entity.star = nil
        end
    end
end

return script
