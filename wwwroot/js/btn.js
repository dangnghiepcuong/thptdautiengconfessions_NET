$(document).ready(function(){
    $('*').on('mouseover', '.btn', function(){
        $(this).css('opacity', '0.7')
    })
    $('*').on('mouseout', '.btn', function(){
        $(this).css('opacity', '1   ')
    })
    $('*').on('mousedown', '.btn', function(){
        $(this).css('opacity', '0.9')
    })
    $('*').on('mouseup', '.btn', function(){
        $(this).css('opacity', '0.7')
    })
})