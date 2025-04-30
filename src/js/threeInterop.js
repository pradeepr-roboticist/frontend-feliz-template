import * as THREE from 'three';

const sceneInstances = new Map();

/**
 * Initializes a Three.js scene with the given id and container
 */
export function initScene(id, containerId, callbacks = {}) {
    const container = document.getElementById(containerId);
    if (!container) {
        console.error(`Container with id ${containerId} not found.`);
        return;
    }

    const scene = new THREE.Scene();
    const camera = new THREE.PerspectiveCamera(75, container.clientWidth / container.clientHeight, 0.1, 1000);
    camera.position.z = 5;

    const renderer = new THREE.WebGLRenderer();
    renderer.setSize(container.clientWidth, container.clientHeight);
    container.appendChild(renderer.domElement);

    const state = {
        scene,
        camera,
        renderer,
        container,
        callbacks,
        objects: new Map()
    };

    // Basic animation loop
    function animate() {
        requestAnimationFrame(animate);
        callbacks.onRender?.();
        renderer.render(scene, camera);
    }

    // Handle resize
    const onResize = () => {
        camera.aspect = container.clientWidth / container.clientHeight;
        camera.updateProjectionMatrix();
        renderer.setSize(container.clientWidth, container.clientHeight);
        callbacks.onResize?.();
    };

    window.addEventListener("resize", onResize);

    sceneInstances.set(id, state);
    animate();
}

/**
 * Call a function with optional arguments on a specific scene instance
 */
export function callSceneFunction(id, functionName, args = []) {
    const instance = sceneInstances.get(id);
    if (!instance) {
        console.warn(`No scene found for ID: ${id}`);
        return;
    }

    switch (functionName) {
        case "loadMesh":
            // console.log(`Loading mesh for scene ${id} with args:`, args);
            let meshId = args[0];
            let geometry = new THREE.BoxGeometry(1, 1, 1);
            const material = new THREE.MeshBasicMaterial({ color: 0x00ff00 });
            const mesh = new THREE.Mesh(geometry, material);
            instance.scene.add(mesh);
            instance.objects.set(meshId, mesh);
            break;

        case "moveObject":
            const [objectId, x, y, z] = args;
            const obj = instance.objects.get(objectId);
            if (obj) obj.position.set(x, y, z);
            break;

        case "logState":
            console.log(instance);
            break;

        default:
            console.warn(`Function ${functionName} not implemented for scene ${id}`);
    }
}

/**
 * Update callback map for an instance
 */
export function updateCallbacks(id, newCallbacks) {
    const instance = sceneInstances.get(id);
    if (instance) {
        Object.assign(instance.callbacks, newCallbacks);
    }
}
