cmake_minimum_required(VERSION 3.12)
project(GenWorld VERSION 0.1.0 LANGUAGES C CXX)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(OpenGL REQUIRED)

# GLFW
set(GLFW_BUILD_DOCS OFF CACHE BOOL "" FORCE)
set(GLFW_BUILD_TESTS OFF CACHE BOOL "" FORCE)
set(GLFW_BUILD_EXAMPLES OFF CACHE BOOL "" FORCE)

include(CTest)
enable_testing()

file(GLOB_RECURSE MY_SOURCES 
    CONFIGURE_DEPENDS 
    ${CMAKE_CURRENT_SOURCE_DIR}/src/*.h
    ${CMAKE_CURRENT_SOURCE_DIR}/src/*.cpp
)

add_executable(${PROJECT_NAME} ${MY_SOURCES} ${PLATFORM_SOURCES} ${GLFW-CMAKE-STARTER-SRC})

add_subdirectory(vendor/glfw)
add_subdirectory(vendor/glad)
add_subdirectory(vendor/glm)

include_directories(${OPENGL_INCLUDE_DIRS})

target_link_libraries(${PROJECT_NAME} 
    PUBLIC glfw 
    PUBLIC glad 
    PUBLIC glm 
    ${OPENGL_LIBRARIES}
)

target_include_directories(${PROJECT_NAME} 
    PUBLIC ${CMAKE_CURRENT_SOURCE_DIR}/include/
)

# Copy assets to build directory.
# This is a temporary solution, we will use CMake to copy assets to the build directory.
# This ensures that we can reference the locations without having to worry about the relative paths.
file(GLOB_RECURSE SHADER_FILES "${CMAKE_CURRENT_SOURCE_DIR}/Shaders/*")
file(GLOB_RECURSE RESOURCE_FILES "${CMAKE_CURRENT_SOURCE_DIR}/Resources/*")
file(GLOB_RECURSE MODEL_FILES "${CMAKE_CURRENT_SOURCE_DIR}/Models/*")
file(GLOB_RECURSE TEXTURE_FILES "${CMAKE_CURRENT_SOURCE_DIR}/Textures/*")

foreach(FILE ${SHADER_FILES})
    file(RELATIVE_PATH REL_PATH "${CMAKE_CURRENT_SOURCE_DIR}/Shaders" "${FILE}")
    configure_file(${FILE} "${CMAKE_CURRENT_BINARY_DIR}/Shaders/${REL_PATH}" COPYONLY)
endforeach()

foreach(FILE ${RESOURCE_FILES})
    file(RELATIVE_PATH REL_PATH "${CMAKE_CURRENT_SOURCE_DIR}/Resources" "${FILE}")
    configure_file(${FILE} "${CMAKE_CURRENT_BINARY_DIR}/Resources/${REL_PATH}" COPYONLY)
endforeach()

foreach(FILE ${MODEL_FILES})
    file(RELATIVE_PATH REL_PATH "${CMAKE_CURRENT_SOURCE_DIR}/Models" "${FILE}")
    configure_file(${FILE} "${CMAKE_CURRENT_BINARY_DIR}/Models/${REL_PATH}" COPYONLY)
endforeach()

foreach(FILE ${TEXTURE_FILES})
    file(RELATIVE_PATH REL_PATH "${CMAKE_CURRENT_SOURCE_DIR}/Textures" "${FILE}")
    configure_file(${FILE} "${CMAKE_CURRENT_BINARY_DIR}/Textures/${REL_PATH}" COPYONLY)
endforeach()

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)