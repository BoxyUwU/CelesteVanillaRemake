local unfixedRotateSpinner = {}

unfixedRotateSpinner.name = "Celestish/UnfixedDustRotateSpinner"
unfixedRotateSpinner.nodeLimits = { 1, 1 }
unfixedRotateSpinner.nodeLineRenderType = "circle"
-- unfixedRotateSpinner.texture = "danger/dustcreature/base00"
unfixedRotateSpinner.depth = -50
unfixedRotateSpinner.placements = {
    name = "Unfixed Dust Rotate Spinner",
    data = {
        clockwise = true,
    }
}

local function getSprite(room, entity, alpha)
    local dustBaseTexture = "danger/dustcreature/base00"
    local dustBaseOutlineTexture = "dust_creature_outlines/base00"

    local drawableSpriteStruct = require("structs.drawable_sprite")
    local dustEdgeColor = { 1.0, 0.0, 0.0 }

    local dustBaseSprite = drawableSpriteStruct.fromTexture(dustBaseTexture, entity)
    local dustBaseOutlineSprite = drawableSpriteStruct.fromInternalTexture(dustBaseOutlineTexture, entity)

    dustBaseOutlineSprite:setColor(dustEdgeColor)

    if alpha then
        dustBaseOutlineSprite:setAlpha(alpha)
        dustBaseSprite:setAlpha(alpha)
    end

    return { dustBaseOutlineSprite, dustBaseSprite }
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
