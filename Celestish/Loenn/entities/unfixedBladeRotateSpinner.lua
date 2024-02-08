local unfixedRotateSpinner = {}

unfixedRotateSpinner.name = "Celestish/UnfixedBladeRotateSpinner"
unfixedRotateSpinner.nodeLimits = { 1, 1 }
unfixedRotateSpinner.nodeLineRenderType = "circle"
-- unfixedRotateSpinner.texture = "danger/blade00"
unfixedRotateSpinner.depth = -50
unfixedRotateSpinner.placements = {
    name = "Unfixed Blade Rotate Spinner",
    data = {
        clockwise = true,
    }
}

local function getSprite(room, entity, alpha)
    local drawableSpriteStruct = require("structs.drawable_sprite")
    local bladeTexture = drawableSpriteStruct.fromTexture("danger/blade00", entity)

    if alpha then
        bladeTexture:setAlpha(alpha)
    end

    return { bladeTexture }
end

function unfixedRotateSpinner.sprite(room, entity)
    return getSprite(room, entity)
end

function unfixedRotateSpinner.nodeSprite(room, entity, node)
    local entityCopy = table.shallowcopy(entity)

    entityCopy.x = node.x
    entityCopy.y = node.y

    return getSprite(room, entityCopy, 0.3)
end

return unfixedRotateSpinner
