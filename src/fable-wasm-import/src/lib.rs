// When the `wee_alloc` feature is enabled, use `wee_alloc` as the global
// allocator.
#[cfg(feature = "wee_alloc")]
#[global_allocator]
static ALLOC: wee_alloc::WeeAlloc = wee_alloc::WeeAlloc::INIT;

extern "C" {
    fn getAddValue() -> i32;
}

#[no_mangle]
pub fn add(x: i32) -> i32 {
    unsafe {
        getAddValue() + x
    }
}
