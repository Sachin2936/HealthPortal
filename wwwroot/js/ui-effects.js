// 🌙 Dark/Light Mode Toggle
document.getElementById('theme-toggle').addEventListener('click', function () {
    document.body.classList.toggle('dark-mode');
    document.body.classList.toggle('light-mode');
});

// 🍃 Floating Leaves
for (let i = 0; i < 12; i++) {
    const leaf = document.createElement('div');
    leaf.className = 'floating-leaf';
    leaf.style.left = `${Math.random() * 100}vw`;
    leaf.style.animationDuration = `${5 + Math.random() * 10}s`;
    document.body.appendChild(leaf);
}

// 💡 Motivational Quote
fetch('https://api.quotable.io/random')
    .then(response => response.json())
    .then(data => {
        document.getElementById('quote').innerText = `"${data.content}" — ${data.author}`;
    });
