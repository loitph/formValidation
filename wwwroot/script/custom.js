document.addEventListener('keydown', function(e) {
  if (e.target.id === 'phone' && (e.key === 'ArrowUp' || e.key === 'ArrowDown')) {
    e.preventDefault();
  }
});
