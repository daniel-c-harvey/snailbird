interface CanvasRenderingContext2D {
    imageSmoothingEnabled: boolean;
}

export function drawImage(canvas: HTMLCanvasElement, base64Image: string): Promise<void> {
    return new Promise((resolve, reject) => {
        const ctx = canvas.getContext('2d');
        if (!ctx) {
            reject(new Error('Unable to get canvas context'));
            return;
        }

        const img = new Image(canvas.width, canvas.height);
        img.onload = () => {
            ctx.clearRect(0, 0, canvas.width, canvas.height);

            // Optional: Configure image rendering
            ctx.imageSmoothingEnabled = true;

            // Center the image and maintain aspect ratio
            const scale = Math.min(
                canvas.width / img.width,
                canvas.height / img.height
            );

            const x = (canvas.width - img.width * scale) / 2;
            const y = (canvas.height - img.height * scale) / 2;

            ctx.drawImage(
                img,
                x, y,
                img.width * scale,
                img.height * scale
            );

            resolve();
        };

        img.onerror = () => reject(new Error('Failed to load image'));
        img.src = base64Image;
    });
}