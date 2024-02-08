local unfixedRotateSpinner = {}

unfixedRotateSpinner.name = "Celestish/UnfixedStarRotateSpinner"
unfixedRotateSpinner.nodeLimits = { 1, 1 }
unfixedRotateSpinner.nodeLineRenderType = "circle"
-- unfixedRotateSpinner.texture = "danger/starfish13"
unfixedRotateSpinner.depth = -50
unfixedRotateSpinner.placements = {
    name = "Unfixed Star Rotate Spinner",
    data = {
        clockwise = true,
    }
}

local function getSprite(room, entity, alpha)
    local drawableSpriteStruct = require("structs.drawable_sprite")
    local starTexture = drawableSpriteStruct.fromTexture("danger/starfish13", entity)

    if alpha then
        starTexture:setAlpha(alpha)
    end

    return { starTexture }
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
